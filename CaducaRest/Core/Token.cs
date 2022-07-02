using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace CaducaRest.Core;

/// <summary>
/// Funciones para generar tokens
/// </summary>
public class Token
{
    /// <summary>
    /// Archivo de configuración
    /// </summary>
    protected readonly IConfiguration Config;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="config"></param>
    public Token(IConfiguration config)
    {
        Config = config;
    }

    /// <summary>
    /// Genera un token
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="fechaExpiracion"></param>
    /// <returns></returns>
    public string GenerarToken(Claim[] claims, DateTime fechaExpiracion)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["Tokens:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken jwtToken = new JwtSecurityToken(Config["Tokens:Issuer"],
                                                        Config["Tokens:Audience"],
                                                        claims,
                                                        expires: fechaExpiracion,
                                                        signingCredentials: creds);
        string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        return token;
    }

    /// <summary>
    /// Refresca un token
    /// </summary>
    /// <returns></returns>
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

