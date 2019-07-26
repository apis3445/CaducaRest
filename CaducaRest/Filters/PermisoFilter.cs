using System;
using System.Security.Claims;
using CaducaRest.Controllers;
using CaducaRest.Core;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CaducaRest.Filters
{
    /// <summary>
    /// Permite validar los permisos en cada controller.
    /// </summary>
    public class PermisoFilter: IActionFilter
    {

        private readonly LocService _localizer;
        private readonly CaducaContext _context;
        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="context"></param>
        /// <param name="localizer">Mensajes de error en diferentes idiomas</param>
        public PermisoFilter(CaducaContext context, LocService localizer)
        {
            this._localizer = localizer;
            this._context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            JsonResult jsonResult;
            CustomError customError = new CustomError(403, "El usuario es inválido");
            //Obtenemos los datos del usuario
            ClaimsPrincipal Usuario = context.HttpContext.User;
            //Se revisa si en el token se incluye el claim con el id del usuario
            if (!Usuario.HasClaim(c => c.Type == ClaimTypes.Sid))
            {
                //regresmos un mensaje de error
                jsonResult = new JsonResult(customError)
                {
                    StatusCode = 403,
                    Value = customError.Message
                };
                context.Result = jsonResult;
                return;
            }
            var usuarioId = Convert.ToInt32(Usuario.FindFirst(c => c.Type == ClaimTypes.Sid).Value);

            //Obtenemos los datos del controller
            var controller = context.Controller as BaseController;
            var metodo = controller.Request.Method.ToLower();

            RolDAO rolDAO = new RolDAO(_context, _localizer);
            bool esAdministrador = rolDAO.EsAdministrador(usuarioId);
            //Si el recurso  requiere un usuario administrador se valida que
            //el usuario sea administrador, si no es asi marcar error
            if (controller.permiso.RequiereAdministrador && !esAdministrador)
            {
                jsonResult = new JsonResult(customError)
                {
                    StatusCode = 403,
                    Value = customError.Message
                };
                context.Result = jsonResult;
                return;
            }
            else
            {
                string operacion = string.Empty;
                //obtenemos el tipo de método que se esta ejecutando
                switch (metodo)
                {
                    case "get":
                        operacion = Operaciones.Consultar.Name;
                        break;
                    case "post":
                        operacion = Operaciones.Crear.Name;
                        break;
                    case "put":
                    case "patch":
                        operacion = Operaciones.Modificar.Name;
                        break;
                    case "delete":
                        operacion = Operaciones.Borrar.Name;
                        break;
                }
                //Se revisa si el usuario tiene autorización para realizar la acción
                RolTablaPermisoDAO rolTablaPermisoDAO = new RolTablaPermisoDAO(_context, _localizer);
                if (!rolTablaPermisoDAO.TienePermiso(usuarioId, controller.permiso.Tabla, operacion))
                {
                    jsonResult = new JsonResult(customError)
                    {
                        StatusCode = 403,
                        Value = customError.Message
                    };
                    context.Result = jsonResult;
                    return;
                }
            }
        }
    }
}