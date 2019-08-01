using System;
using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Mvc;

namespace CaducaRest.Controllers
{
public class BaseController : Controller
{
    /// <summary>
    /// Objeto para conectarse al a base de datos
    /// </summary>
    private readonly CaducaContext _context;

    private readonly LocService _localizer;
    /// <summary>
    /// Permite registrar la página para validar sus permisos
    /// </summary>
    public PermisoDTO permiso;

    public int Id;

        public string Observaciones;

    public BaseController(CaducaContext context, LocService localizer)
    {
        this._context = context;
        this._localizer = localizer;
        permiso = new PermisoDTO();
    }

}

}