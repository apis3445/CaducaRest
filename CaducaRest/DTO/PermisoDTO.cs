namespace CaducaRest.DTO;

/// <summary>
/// Permite validar los permisos de cada servicio
/// </summary>
public class PermisoDTO
{
    /// <summary>
    /// Nombre de la tabla a validar
    /// </summary>
    public string Tabla { get; set; }

    /// <summary>
    /// Si esta en true se requiere permiso de Administrador
    /// </summary>
    public bool RequiereAdministrador { get; set; } = false;
}
