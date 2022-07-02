using System.Linq;
using CaducaRest.Models;
using CaducaRest.Resources;

namespace CaducaRest.DAO;

/// <summary>
/// Guarda las operaciones que puede realizar un usuario en una página
/// </summary>
public class RolTablaPermisoDAO
{
    private readonly CaducaContext contexto;
    private readonly LocService localizacion;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="localize"></param>
    public RolTablaPermisoDAO(CaducaContext context, LocService localize)
    {
        this.contexto = context;
        this.localizacion = localize;
    }

    /// <summary>
    /// Indica si el usuario tiene permiso de realizar la operación
    /// (alta,baja, cambio) en una tabla
    /// </summary>
    /// <param name="usuarioId">Id del usuario</param>
    /// <param name="tabla">Nombre de la tabla que se va a modificar</param>
    /// <param name="operacion">Operación a realizar</param>
    /// <returns></returns>
    public bool TienePermiso(int usuarioId, string tabla, string operacion)
    {
        var permiso = (from Tabla in contexto.Tabla
                       join TablaPermiso in contexto.TablaPermiso
                        on Tabla.Id equals TablaPermiso.TablaId
                       join Permiso in contexto.Permiso
                        on TablaPermiso.PermisoId equals Permiso.Id
                       join RolTablaPermiso in contexto.RolTablaPermiso
                           on TablaPermiso.Id equals RolTablaPermiso.TablaPermisoId
                       join UsuarioRol in contexto.UsuarioRol
                           on RolTablaPermiso.RolId equals UsuarioRol.RolId
                       where Tabla.Nombre == tabla
                        && Permiso.Nombre == operacion
                        && UsuarioRol.UsuarioId == usuarioId
                        && RolTablaPermiso.TienePermiso
                       select RolTablaPermiso.Id).FirstOrDefault();
        return permiso > 0;
    }
}
