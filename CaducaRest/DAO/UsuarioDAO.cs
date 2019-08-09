using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public CustomError customError;
        public TokenDTO tokenDTO;
        private readonly AccesoDAO<Usuario> usuarioDAO;

        public UsuarioDAO(CaducaContext context, LocService localize)
        {
            this.contexto = context;
            this.localizacion = localize;
        }

        public async Task<List<Usuario>> ObtenerTodoAsync()
        {
            return await usuarioDAO.ObtenerTodoAsync();
        }

        public async Task<Usuario> ObtenerPorIdAsync(int id)
        {
            return await usuarioDAO.ObtenerPorIdAsync(id);
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

public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO, IConfiguration config)
{
    Seguridad seguridad = new Seguridad();           
    var usuario = await contexto.Usuario.FirstOrDefaultAsync(usu => usu.Clave == loginDTO.Usuario);
    if (usuario == null)
    {
        customError = new CustomError(400,
            String.Format(this.localizacion.GetLocalizedHtmlString("GeneralNoExiste"),
                    "La clave del usuario"));
        return tokenDTO;
    }
    if (usuario.Password != seguridad.GetHash(usuario.Adicional1 + loginDTO.Password))
    {
        customError = new CustomError(400,
                    this.localizacion.GetLocalizedHtmlString("PasswordIncorrecto"));
        return tokenDTO;
    }
    if (!usuario.Activo)
    {
        customError = new CustomError(403,
                    this.localizacion.GetLocalizedHtmlString("UsuarioInactivo"));
        return tokenDTO;
    }
    tokenDTO = GenerarToken(config, usuario.Id, usuario.Nombre);
    var usuarioAcceso = new UsuarioAcceso();
    usuarioAcceso.UsuarioId = usuario.Id;       
    usuarioAcceso.Fecha = DateTime.Now;
    usuarioAcceso.Token = tokenDTO.Token;
    usuarioAcceso.Activo = true;
    usuarioAcceso.Ciudad = "Default";
    usuarioAcceso.Estado = "Default";
    usuarioAcceso.SistemaOperativo = "Default";
    usuarioAcceso.RefreshToken = tokenDTO.RefreshToken;
    usuarioAcceso.Navegador = "Default";
    contexto.UsuarioAcceso.Add(usuarioAcceso);
    contexto.SaveChanges();
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
    }
}
