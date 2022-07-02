using System;
using System.Security.Claims;
using System.Threading.Tasks;
using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.DAO;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CaducaRest.Core;

/// <summary>
/// Permite revisar la seguridad en los servicios
/// </summary>
public class PermisoEditHandler : AuthorizationHandler<OperationAuthorizationRequirement, PermisoDTO>
{
    private readonly CaducaContext contexto;
    private readonly LocService _localizer;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context">Contexto de la bd</param>
    /// <param name="localizer">Idiomas</param>
    public PermisoEditHandler(CaducaContext context, LocService localizer)
    {
        contexto = context;
        _localizer = localizer;
    }

    /// <summary>
    /// Permite agregar reglas para validar los permisos del usuario
    /// </summary>
    /// <param name="context"></param>
    /// <param name="operacion"></param>
    /// <param name="recurso"></param>
    /// <returns></returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement operacion, PermisoDTO recurso)
    {
        var usuarioId = Convert.ToInt32(context.User.FindFirst(c => c.Type == ClaimTypes.Sid).Value);
        //Se revisa si en el token se incluye el claim con el id del usuario
        if (!context.User.HasClaim(c => c.Type == ClaimTypes.Sid))
            context.Fail();
        RolDAO rolDAO = new RolDAO(contexto, _localizer);
        bool esAdministrador = rolDAO.EsAdministrador(usuarioId);
        //Si el recurso  requiere un usuario administrador se valida que
        //el usuario sea administrador, si no es asi marcar error
        if (recurso.RequiereAdministrador && !esAdministrador)
            context.Fail();
        else
        {
            if (!esAdministrador)
            {
                //Se revisa si el usuario tiene autorización para realizar la acción
                RolTablaPermisoDAO rolTablaPermisoDAO = new RolTablaPermisoDAO(contexto, _localizer);
                if (!rolTablaPermisoDAO.TienePermiso(usuarioId, recurso.Tabla, operacion.Name))
                    context.Fail();
                else
                    context.Succeed(operacion);
            }
            context.Succeed(operacion);
        }
        return Task.CompletedTask;
    }
}
