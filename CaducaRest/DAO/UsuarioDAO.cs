using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
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

        public async Task<TokenDTO> LoginAsync(LoginDTO loginDTO, IConfiguration config)
        {
            TokenDTO tokenDTO = new TokenDTO();
            Seguridad seguridad = new Seguridad();
            Token token = new Token(config);
            var usuario = await contexto.Usuario.FirstOrDefaultAsync
                    (usu => usu.Clave == loginDTO.Usuario);
            if (usuario == null)
            {
                customError = new CustomError(400,
                    String.Format(this.localizacion.GetLocalizedHtmlString("GeneralNoExiste")
                    , "La clave del usuario"));
                return tokenDTO;
            }
            if (usuario.Password != seguridad.GetHash(usuario.Adicional1 + loginDTO.Password))
            {
                customError = new CustomError(400, this.localizacion.GetLocalizedHtmlString("PasswordIncorrecto"));
                return tokenDTO;
            }
            if (!usuario.Activo)
            {
                customError = new CustomError(403, this.localizacion.GetLocalizedHtmlString("UsuarioInactivo"));
                return tokenDTO;
            }           
            var claims = new Claim[]
            {
                new Claim(ClaimTypes.Sid, usuario.Id.ToString()),
            };
            DateTime fechaExpiracion = DateTime.Now.AddDays(15).ToLocalTime();
            tokenDTO.Token = token.GenerarToken(claims, fechaExpiracion);
            tokenDTO.TokenExpiration = fechaExpiracion;
            tokenDTO.UsuarioId = usuario.Id;
            tokenDTO.RefreshToken = token.RefrescarToken();
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
