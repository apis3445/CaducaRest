using System;
using System.Net;
using System.Net.Mail;

namespace CaducaRest.Core
{
public class Correo
{
/// <summary>
/// Mensaje del correo
/// </summary>
public string Mensaje;
/// <summary>
/// Correos a quien se enviara el correo
/// </summary>
public string Para;
/// <summary>
/// Asunto del correo
/// </summary>
public string Asunto;

public Correo()
{

}

/// <summary>
/// Permite enviar un correo
/// </summary>
public void Enviar()
{
    string smtpAddress, usuarioCorreo, passwordCorreo;
    int puerto = 587;            
    smtpAddress = "smtp.gmail.com";
    usuarioCorreo = "correo@gmail.com";
    passwordCorreo = "tupassword";

    SmtpClient client = new SmtpClient(smtpAddress, puerto)
    {
        Credentials = new NetworkCredential(usuarioCorreo, passwordCorreo),
        EnableSsl = true,
    };
    MailMessage mailMessage = new MailMessage
    {
        From = new MailAddress(usuarioCorreo)
    };
    mailMessage.To.Add(Para);
    mailMessage.IsBodyHtml = true;
    mailMessage.Body = Mensaje;
    mailMessage.Subject = Asunto;
    try
    {
        client.Send(mailMessage);
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.InnerException);
    }
}
    }
}