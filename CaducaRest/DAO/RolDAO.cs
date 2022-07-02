using System.Collections.Generic;
using CaducaRest.Models;
using CaducaRest.Resources;
using System.Linq;

namespace CaducaRest.DAO;

/// <summary>
/// Funciones de acceso a datos para los roles
/// </summary>
public class RolDAO
{
    private readonly CaducaContext contexto;
    private readonly LocService localizacion;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="localize"></param>
    public RolDAO(CaducaContext context, LocService localize)
    {
        this.contexto = context;
        this.localizacion = localize;
    }

    /// <summary>
    /// Permite obtener los roles de un usuario
    /// </summary>
    /// <param name="usuarioId">Id del usuario</param>
    /// <returns></returns>
    public List<string> ObtenerRolesPorUsuarios(int usuarioId)
    {
        return (from usuarioRol in contexto.UsuarioRol
                join rol in contexto.Rol
                    on usuarioRol.RolId equals rol.Id
                where usuarioRol.UsuarioId == usuarioId
                select rol.Nombre).ToList();
    }

    /// <summary>
    /// Indica si el usuario tiene el rol administrador
    /// </summary>
    /// <param name="usuarioId">Id del usuario</param>
    /// <returns></returns>
    public bool EsAdministrador(int usuarioId)
    {
        var total = (from usuarioRol in contexto.UsuarioRol
                     join rol in contexto.Rol
                         on usuarioRol.RolId equals rol.Id
                     where usuarioRol.UsuarioId == usuarioId
                         && rol.Nombre == "Administrador"
                     select usuarioRol.Id).Count();
        if (total > 0)
            return true;
        return false;
    }
}