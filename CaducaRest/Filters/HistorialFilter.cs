using System;
using System.Linq;
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
    public class HistorialFilter : IActionFilter
    {
        private readonly LocService _localizer;
        private readonly CaducaContext _context;
        /// <summary>

        public HistorialFilter(CaducaContext context, LocService localizer)
        {
            this._localizer = localizer;
            this._context = context;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            bool correcto = false;
            correcto = context.Result is OkObjectResult
                || context.Result is NoContentResult
                || context.Result is CreatedAtActionResult;

            if (correcto)
            {
                ClaimsPrincipal Usuario = context.HttpContext.User;
                var controller = context.Controller as BaseController;
                string metodo = controller.Request.Method.ToLower();
                //Identificamos de acuerdo al método la acción
                //realizada
                int actividad;
                switch (metodo)
                {
                    case "post":
                        actividad = (int)ActividadEnumeration.Insertar;
                        break;

                    case "put":
                    case "patch":
                        actividad = (int)ActividadEnumeration.Modficar;
                        break;
                    case "delete":
                        actividad = (int)ActividadEnumeration.Borrar;
                        break;
                    default:
                        actividad = 0;
                        break;
                }
                int UsuarioId = 0;
                if (correcto)
                {
                    int.TryParse(Usuario.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid).Value, out UsuarioId);
                    if (UsuarioId > 0 && controller.Id > 0)
                    {
                        HistorialDAO historialDAO = new HistorialDAO(_context, _localizer);
                        if (actividad > 0)
                            _ = historialDAO.AgregarAsync(UsuarioId,
                                                            actividad,
                                                            controller.permiso.Tabla,
                                                            controller.Id,
                                                            controller.Observaciones).Result;
                        else
                        {
                            if (actividad < 0)
                                _ = historialDAO.BorraAsync(controller.permiso.Tabla, controller.Id).Result;
                        }
                    }
                }
            }

        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
