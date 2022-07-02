using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CaducaRest.Core;
using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CaducaRest.DAO;
/// <summary>
/// Funciones de acceso a datos para la tabla usuario
/// </summary>
public class UsuarioDAO
{
    private readonly CaducaContext contexto;
    private readonly LocService localizacion;
    private readonly string _path;
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Mensaje de error
    /// </summary>
    public CustomError customError;
    /// <summary>
    /// Token
    /// </summary>
    public TokenDTO tokenDTO;
    private readonly AccesoDAO<Usuario> usuarioDAO;

    private const int MAXIMOS_INTENTOS = 5;
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="localize"></param>
    /// <param name="path"></param>
    /// z
    /// <param name="configuration"></param>
    public UsuarioDAO(CaducaContext context, LocService localize, string path, IConfiguration configuration)
    {
        this.contexto = context;
        this.localizacion = localize;
        this.tokenDTO = new TokenDTO();
        this._path = path;
        this.Configuration = configuration;
        usuarioDAO = new AccesoDAO<Usuario>(context, localize);
    }

    /// <summary>
    /// Obtiene todos los usuarios
    /// </summary>
    /// <returns></returns>
    public async Task<List<Usuario>> ObtenerTodoAsync()
    {
        return await usuarioDAO.ObtenerTodoAsync();
    }
    /// <summary>
    /// Obtiene un usuario por id
    /// </summary>
    /// <param name="id">Id del usuario</param>
    /// <returns></returns>
    public async Task<Usuario> ObtenerPorIdAsync(int id)
    {
        return await usuarioDAO.ObtenerPorIdAsync(id);
    }

    /// <summary>
    /// Obtiene los datos de un usuario de acuerdo a su clave
    /// </summary>
    /// <param name="clave">Clave del usuario</param>
    /// <returns></returns>
    public async Task<Usuario> ObtenerPorClave(string clave)
    {
        var usuario = await contexto.Usuario.FirstOrDefaultAsync(usu => usu.Clave == clave);
        if (usuario == null)
        {
            customError = new CustomError(400,
                            String.Format(this.localizacion
                                      .GetLocalizedHtmlString("GeneralNoExiste"),
                                    "La clave del usuario"));
        }
        return usuario;
    }

    /// <summary>
    /// Valida que el usuario este activo
    /// </summary>
    /// <param name="usuario">Datos del usuario</param>
    /// <returns></returns>
    public bool EsUsuarioActivo(Usuario usuario)
    {

        if (!usuario.Activo)
        {
            customError = new CustomError(403,
            this.localizacion.GetLocalizedHtmlString("UsuarioInactivo"));
            return false;
        }

        return true;
    }

    /// <summary>
    /// El usuario esta bloqueado si ya tiene un código
    /// </summary>
    /// <param name="usuario">Datos del usuario</param>
    /// <returns></returns>
    public bool EsUsuarioBloqueado(Usuario usuario)
    {
        return usuario.Codigo > 0;
    }

    /// <summary>
    /// Valida el password del usuario
    /// </summary>
    /// <param name="usuario">Datos del usuario</param>
    /// <param name="password">password del usuario</param>
    /// <returns></returns>
    public bool EsPasswordCorrecto(Usuario usuario, string password)
    {
        Seguridad seguridad = new Seguridad();
        return usuario.Password == seguridad.GetHash(usuario.Adicional1 + password);
    }

    /// <summary>
    /// Indica si el password es correcto, si no esta bloqueado el acceso
    /// por intentos incorrectos
    /// </summary>
    /// <param name="usuario">Datos del usuario</param>
    /// <param name="password">Password</param>
    /// <param name="codigo">Código para desbloquear el usuario</param>
    /// <returns></returns>
    public bool EsPasswordValido(Usuario usuario, string password, int codigo)
    {
        if (EsUsuarioBloqueado(usuario))
        {
            if (EsPasswordCorrecto(usuario, password) && usuario.Codigo == codigo)
            {
                //Reiniciamos el número de intentos y el código para iniciar sesión
                usuario.Intentos = 0;
                usuario.Codigo = 0;
                contexto.SaveChanges();
                return true;
            }
            else
            {
                customError = new CustomError(423,
                this.localizacion.GetLocalizedHtmlString("PasswordLocked"));
                return false;
            }
        }
        else
        {
            if (!EsPasswordCorrecto(usuario, password))
            {
                usuario.Intentos = usuario.Intentos + 1;
                if (usuario.Intentos > MAXIMOS_INTENTOS)
                {
                    Random r = new Random();
                    codigo = r.Next(0, 999999);
                    usuario.Codigo = codigo;
                    customError = new CustomError(423,
                                        this.localizacion.GetLocalizedHtmlString("PasswordLocked"));
                    EnviaCorreoIntentosIncorrectos(_path, usuario.Clave, usuario.Email, codigo);
                }
                else
                {
                    customError = new CustomError(400,
                        this.localizacion.GetLocalizedHtmlString("PasswordIncorrecto"));
                }
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Permite validar un token para refrescarlo
    /// </summary>
    /// <param name="refreshToken">Código para refrescar el token</param>
    /// <param name="config">Archivo de configuración</param>
    /// <returns></returns>
    public bool ValidarToken(string refreshToken, IConfiguration config)
    {
        //Buscamos el código para refrescar el token enviado
        var tokenGuardado = contexto.UsuarioAcceso.FirstOrDefault(u => u.RefreshToken == refreshToken && u.Activo);
        //No existe marcamos error
        if (tokenGuardado == null)
            return false;
        //Revisamos que el usuario este activo
        var usuario = contexto.Usuario.Find(tokenGuardado.UsuarioId);
        if (usuario == null || !usuario.Activo)
            return false;
        tokenDTO = GenerarToken(config, usuario.Id, usuario.Nombre);
        return true;
    }

    /// <summary>
    /// Login del usuario
    /// </summary>
    /// <param name="loginDTO">Datos del login</param>
    /// <param name="config">Archivo de configuración</param>
    /// <param name="ip">IP de la cual se realiza el login</param>
    /// <param name="navegador">Navegador del cual se realiza el login</param>
    /// <returns></returns>
    public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO, IConfiguration config, string ip, string navegador)
    {
        var usuario = await ObtenerPorClave(loginDTO.Usuario);
        if (usuario == null)
            return tokenDTO;
        if (!EsUsuarioActivo(usuario))
            return tokenDTO;
        if (!EsPasswordValido(usuario, loginDTO.Password, loginDTO.Codigo))
            return tokenDTO;
        tokenDTO = GenerarToken(config, usuario.Id, usuario.Nombre);

        UsuarioAccesoDAO usuarioAccesoDAO = new UsuarioAccesoDAO(contexto, localizacion);
        var esOtroNavegador = usuarioAccesoDAO.EsOtroNavegador(navegador, usuario.Id);
        var esOtraCiudad = await usuarioAccesoDAO.EsOtraCiudadAsync(ip, usuario.Id);
        if (esOtroNavegador || esOtraCiudad)
            EnviaCorreoNuevoAcceso(_path, usuario.Clave, usuario.Email, usuarioAccesoDAO.datosIP, ip, navegador);
        await usuarioAccesoDAO.GuardarAccesoAsync(tokenDTO, usuario.Id, ip, navegador);
        return tokenDTO;
    }

    /// <summary>
    /// Genera un token
    /// </summary>
    /// <param name="config">Archivo de configuración</param>
    /// <param name="usuarioId">Id del usuario</param>
    /// <param name="nombre">Nombre del usuario</param>
    /// <returns></returns>
    public TokenDTO GenerarToken(IConfiguration config, int usuarioId, string nombre)
    {
        Token token = new Token(config);
        tokenDTO = new TokenDTO();
        var claims = new List<Claim>
            {
                    new Claim(ClaimTypes.Sid, usuarioId.ToString()),
            };
        RolDAO rolDAO = new RolDAO(contexto, localizacion);
        var roles = rolDAO.ObtenerRolesPorUsuarios(usuarioId);

        foreach (var rol in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, rol));
            if (rol == "Vendedor" || rol == "Administrador")
            {
                var totalCategorias = contexto.UsuarioCategoria
                                            .Where(u => u.UsuarioId == usuarioId).Count();
                if (totalCategorias > 0)
                    claims.Add(new Claim("Categorias", totalCategorias.ToString()));
            }
        }

        DateTime fechaExpiracion = DateTime.Now.AddDays(15).ToLocalTime();
        tokenDTO.Token = token.GenerarToken(claims.ToArray(), fechaExpiracion);
        tokenDTO.TokenExpiration = fechaExpiracion;
        tokenDTO.UsuarioId = usuarioId;
        tokenDTO.RefreshToken = token.RefrescarToken();
        tokenDTO.Nombre = nombre;
        return tokenDTO;
    }

    /// <summary>
    /// Permite agregar un usuario
    /// </summary>
    /// <param name="usuario">Datos del usuario a agregar</param>
    /// <returns></returns>
    public async Task<bool> AgregarAsync(Usuario usuario)
    {
        contexto.Usuario.Add(usuario);
        Seguridad seguridad = new Seguridad();
        usuario.Adicional1 = seguridad.GetSalt();
        usuario.Password = seguridad.GetHash(usuario.Adicional1 + usuario.Password);
        await contexto.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Envia un correo cuando se alcanza el máximo número de intentos incorrectos
    /// </summary>
    /// <param name="path">Ruta para los archivos template</param>
    /// <param name="usuario">Usuario</param>
    /// <param name="email">Email</param>
    /// <param name="codigo">Código para desbloquear </param>
    private void EnviaCorreoIntentosIncorrectos(string path, string usuario, string email, int codigo)
    {
        string body = System.IO.File.ReadAllText(Path.Combine(path, "Templates", "IntentosIncorrectos.html"));
        body = body.Replace("{{usuario}}", usuario);
        body = body.Replace("{{codigo}}", codigo.ToString());
        Correo mail = new Correo(Configuration)
        {
            Para = email,
            Mensaje = body,
            Asunto = "Tu cuenta ha sido bloqueada"
        };
        try
        {
            mail.Enviar();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException);
        }
    }

    /// <summary>
    /// Envia correo cuando se entra al sistema de otra ciudad
    /// </summary>
    /// <param name="path">Ruta para los templates</param>
    /// <param name="usuario">Usuario</param>
    /// <param name="email">Email</param>
    /// <param name="datosIP">Datos de la ip</param>
    /// <param name="ip">Ip</param>
    /// <param name="navegador">Navegador</param>
    private void EnviaCorreoNuevoAcceso(string path, string usuario, string email, DatosIPDTO datosIP, string ip, string navegador)
    {
        string body = System.IO.File.ReadAllText(Path.Combine(path, "Templates", "NuevoAcceso.html"));
        body = body.Replace("{{usuario}}", usuario);
        body = body.Replace("{{ciudad}}", datosIP.city);
        body = body.Replace("{{estado}}", datosIP.subdivision);
        body = body.Replace("{{pais}}", datosIP.country);
        body = body.Replace("{{ip}}", ip);
        body = body.Replace("{{navegador}}", navegador);
        body = body.Replace("{{fecha}}", DateTime.Now.ToLongDateString() + " " + DateTime.Now.ToLongTimeString());

        Correo mail = new Correo(Configuration)
        {
            Para = email,
            Mensaje = body,
            Asunto = "Nuevo acceso en " + datosIP.city
        };
        try
        {
            mail.Enviar();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.InnerException);
        }
    }
}