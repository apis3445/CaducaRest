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

namespace CaducaRest.DAO
{
    public class UsuarioDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        private readonly string _path;

        public CustomError customError;
        public TokenDTO tokenDTO;
        private readonly AccesoDAO<Usuario> usuarioDAO;
        public const int MAXIMOS_INTENTOS = 5;
        public UsuarioDAO(CaducaContext context, LocService localize, string path)
        {
            this.contexto = context;
            this.localizacion = localize;
            this.tokenDTO = new TokenDTO();
            this._path = path;
            usuarioDAO = new AccesoDAO<Usuario>(context, localize);
        }

        public async Task<List<Usuario>> ObtenerTodoAsync()
        {
            return await usuarioDAO.ObtenerTodoAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            return await usuarioDAO.ObtenerPorIdAsync(id);
        }

        public async Task<Usuario> ObtenerPorClave(string clave)
        {
            var usuario = await contexto.Usuario.FirstOrDefaultAsync(usu => usu.Clave == clave);
            if (usuario==null)
            {
                customError = new CustomError(400,
                                String.Format(this.localizacion
                                          .GetLocalizedHtmlString("GeneralNoExiste"),
                                        "La clave del usuario"));
            }
            return usuario;
        }

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

        public bool EsUsuarioBloqueado(Usuario usuario)
        {
            return usuario.Codigo > 0;
        }

        public bool EsPasswordCorrecto(Usuario usuario, string password)
        {
            Seguridad seguridad = new Seguridad();
            return usuario.Password == seguridad.GetHash(usuario.Adicional1 + password);
        }
       
        public bool EsPasswordValido(Usuario usuario, string password, int codigo )
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
                }
            }
            return true;
        }

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

        public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO, IConfiguration config, string ip)
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
            await usuarioAccesoDAO.AgregarAsync(tokenDTO, usuario.Id, ip);
            return tokenDTO;
        }

        
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

        public async Task<bool> AgregarAsync(Usuario usuario)
        {
            contexto.Usuario.Add(usuario);
            Seguridad seguridad = new Seguridad();
            usuario.Adicional1 = seguridad.GetSalt();
            usuario.Password = seguridad.GetHash(usuario.Adicional1 + usuario.Password);
            await contexto.SaveChangesAsync();
            return true;
        }

        private void EnviaCorreoIntentosIncorrectos(string path, string usuario, string email, int codigo)
        {
            string body = System.IO.File.ReadAllText(Path.Combine(path, "Templates", "IntentosIncorrectos.html"));
            body = body.Replace("{{usuario}}", usuario);
            body = body.Replace("{{codigo}}", codigo.ToString());
            Correo mail = new Correo()
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
    }
}