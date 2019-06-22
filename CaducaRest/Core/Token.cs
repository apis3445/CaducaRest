using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CaducaRest.Core
{
    public class Token
    {
        protected readonly IConfiguration Config;

        public Token(IConfiguration config)
        {
            Config = config;
        }

        public string GenerarToken(Claim[] claims, DateTime fechaExpiracion)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Tokens:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken jwtToken = new JwtSecurityToken(Config["Tokens:Issuer"],
                                                            Config["Tokens:Issuer"],
                                                            claims,
                                                            expires: fechaExpiracion,
                                                            signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return token;
        }

        public string RefrescarToken()
        {
            var randomNumber = new byte[32];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber).Replace("$", "4").Replace("/", "1").Replace("&", "6").Replace("+", "9").Replace("-", "5");
            }
        }
    }
}
