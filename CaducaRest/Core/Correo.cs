using System;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Configuration;

namespace CaducaRest.Core
{

    /// <summary>
    /// Funciones para enviar correos
    /// </summary>
    public class Correo
    {
        private IConfiguration Configuration { get; }
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

        public Correo(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Permite enviar un correo
        /// </summary>
        public void Enviar()
        {
            string smtpAddress, usuarioCorreo, passwordCorreo;
            int puerto = 587;
            smtpAddress = Configuration["Correo:smtp"];
            usuarioCorreo = Configuration["Correo:correo"];
            passwordCorreo = Configuration["Correo:pass"];

            SmtpClient client = new SmtpClient(smtpAddress, puerto)
            {
                Credentials = new NetworkCredential(usuarioCorreo, passwordCorreo),
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
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