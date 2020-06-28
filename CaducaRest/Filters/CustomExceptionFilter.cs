using CaducaRest.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Filters
{
    /// <summary>
    /// Filtro para errores persomnalizados
    /// </summary>
    public class CustomExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IModelMetadataProvider _modelMetadataProvider;
     

        /// <summary>
        /// Filtro para controlar todas las excepciones ocurridas en el sistema
        /// </summary>
        /// <param name="hostingEnvironment">Para saber si el ambiente es producción o desarrolo</param>
        /// <param name="modelMetadataProvider">Datos acerca del modeolo</param>
        public CustomExceptionFilter(IWebHostEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider)
        {
            _hostingEnvironment = hostingEnvironment;
            _modelMetadataProvider = modelMetadataProvider;
        }

        /// <summary>
        /// Acciones a realizar cuando ocurre una excepción
        /// </summary>
        /// <param name="context">Datos de la excepción</param>
        public override void OnException(ExceptionContext context)
        {
            if (context.Exception.InnerException != null && context.Exception.InnerException is MySqlException)
            {
                Correo mail = new Correo()
                {
                    Para = "abigail_armijo@yahoo.com",
                    Mensaje = ex.InnerException.ToString(),
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
                string tabla = " el/la " + context.RouteData.Values["controller"].ToString() + " ";
                MySqlException exMySql = (MySqlException)context.Exception.InnerException;
                CustomMySQLException mySqlCustomError = new CustomMySQLException();
                var mensajeError = mySqlCustomError.MuestraErrorMYSQL(exMySql, tabla, this.GetType().Name);              
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
