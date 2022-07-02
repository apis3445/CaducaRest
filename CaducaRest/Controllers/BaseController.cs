using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CaducaRest.Controllers;

/// <summary>
/// Funciones generales para los controladores como historial de cambios
/// </summary>
public class BaseController : Controller
{
    /// <summary>
    /// Objeto para conectarse al a base de datos
    /// </summary>
    protected CaducaContext _context;

    /// <summary>
    /// Objeto para mensajes de error en varios idiomas
    /// </summary>
    protected LocService _localizer;

    /// <summary>
    /// Permite registrar la página para validar sus permisos
    /// </summary>
    public PermisoDTO permiso;

    /// <summary>
    /// Id para guardar el historial de cambios
    /// </summary>
    public int Id;

    /// <summary>
    /// Observaciones para el historial de cambios
    /// </summary>
    public string Observaciones;

    /// <summary>
    /// Objeto para guardar el log de errores
    /// </summary>
    protected readonly ILogger logger;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>  
    /// <param name="logger"></param>
    /// <param name="localizer"></param>
    public BaseController(CaducaContext context, ILogger<BaseController> logger, LocService localizer)
    {
        this._context = context;
        this._localizer = localizer;
        this.logger = logger;
        permiso = new PermisoDTO();
    }
}