using CaducaRest.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System;

namespace CaducaRest.Filters
{
    /// <summary>
    /// Filtro para errores persomnalizados
    /// </summary>
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
        private IConfiguration Configuration { get; }

        /// <summary>
        /// Filtro para controlar todas las excepciones ocurridas en el sistema
        /// </summary>
        /// <param name="hostingEnvironment">Para saber si el ambiente es producción o desarrolo</param>
        /// <param name="modelMetadataProvider">Datos acerca del modeolo</param>
        /// <param name="configuration"></param>
        public CustomExceptionFilter(IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider, IConfiguration configuration)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
            Configuration = configuration;
        }

        /// <summary>
        /// Acciones a realizar cuando ocurre una excepción
        /// </summary>
        /// <param name="context">Datos de la excepción</param>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.InnerException != null && context.Exception.InnerException is MySqlException)
            {
                Correo mail = new Correo(Configuration)
                {
                    Para = "abigail_armijo@yahoo.com",
                    Mensaje = context.Exception.InnerException.ToString(),
                    Asunto = "Error en Caduca Rest"
                };
                try
                {
                    mail.Enviar();
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1.InnerException);
                }
                string mensajeError;
                if (context.RouteData.Values["controller"] != null)
                {
                    string tabla = " el/la " + context.RouteData.Values["controller"].ToString() + " ";
                    MySqlException exMySql = (MySqlException)context.Exception.InnerException;
                    CustomMySQLException mySqlCustomError = new CustomMySQLException();
                    mensajeError = mySqlCustomError.MuestraErrorMYSQL(exMySql, tabla, this.GetType().Name);                  
                }
                else
                {
                    mensajeError = "Ocurrio un error y ha sido registrado";
                }
                BadRequestObjectResult badRequest = new BadRequestObjectResult(new CustomError(400, mensajeError));
                context.Result = badRequest;
            }    
            else
            {
                context.Result = new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
