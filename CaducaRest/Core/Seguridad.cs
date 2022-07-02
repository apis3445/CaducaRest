using System;
using System.Security.Cryptography;
using System.Text;

namespace CaducaRest.Core;

/// <summary>
/// Funciones de seguridad
/// </summary>
public class Seguridad
{
    /// <summary>
    /// Obtiene el salt para los password
    /// </summary>
    /// <returns></returns>
    public string GetSalt()
    {
        byte[] bytes = new byte[128 / 8];
        using (var keyGenerator = RandomNumberGenerator.Create())
        {
            keyGenerator.GetBytes(bytes);
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }

    /// <summary>
    /// Obtiene el hash de un texto
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    public string GetHash(string text)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
        }
    }

}

