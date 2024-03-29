﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CaducaRest.Models;
using CaducaRest.Resources;
using CaducaRest.DAO;
using Microsoft.AspNetCore.Authorization;
using CaducaRest.Filters;
using Microsoft.Extensions.Logging;

namespace CaducaRest.Controllers;
/// <summary>
/// Servicios para los productos
/// </summary>
[Route("api/[controller]")]
[ApiController]
[Authorize(Roles = "Administrador,Vendedor")]
[TypeFilter(typeof(PermisoFilter))]
[TypeFilter(typeof(HistorialFilter))]
public class ProductosController : BaseController
{
    private ProductoDAO productoDAO;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="logger"></param>
    /// <param name="localize"></param>
    public ProductosController(CaducaContext context, ILogger<UsuariosController> logger, LocService localize)
            : base(context, logger, localize)
    {
        _context = context;
        productoDAO = new ProductoDAO(context, localize);
        this.permiso.Tabla = "Producto";
        this.permiso.RequiereAdministrador = false;
    }

    /// <summary>
    /// Obtener todos los productos
    /// </summary>
    /// <returns></returns>
    [Authorize(Policy = "VendedorConCategorias")]
    [HttpGet]
    public IEnumerable<Producto> GetProducto()
    {
        return productoDAO.ObtenerTodo();
    }

    /// <summary>
    /// Obtener un producto por su Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // GET: api/Productos/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProducto([FromRoute] int id)
    {
        var producto = await productoDAO.ObtenerPorIdAsync(id);

        if (producto == null)
        {
            return NotFound();
        }

        return Ok(producto);
    }

    /// <summary>
    /// Actualizar un producto
    /// </summary>
    /// <param name="id">Id del producto</param>
    /// <param name="producto">Datos del producto</param>
    /// <returns></returns>
    // PUT: api/Productos/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto([FromRoute] int id, [FromBody] Producto producto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        if (id != producto.Id)
        {
            return BadRequest();
        }

        if (!await productoDAO.ModificarAsync(producto))
        {
            return StatusCode(productoDAO.customError.StatusCode,
                              productoDAO.customError.Message);
        }
        Id = producto.Id;
        return NoContent();
    }

    /// <summary>
    /// Agregar un producto
    /// </summary>
    /// <param name="producto">Datos del producto a agregar</param>
    /// <returns></returns>
    // POST: api/Productos
    [HttpPost]
    public async Task<IActionResult> PostProducto([FromBody] Producto producto)
    {
        if (!await productoDAO.AgregarAsync(producto))
        {
            return StatusCode(productoDAO.customError.StatusCode,
                              productoDAO.customError.Message);
        }
        Id = producto.Id;
        return CreatedAtAction("GetProducto", new { id = producto.Id }, producto);
    }

    /// <summary>
    /// Borrar un producto
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    // DELETE: api/Productos/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto([FromRoute] int id)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        if (!await productoDAO.BorraAsync(id))
        {
            return StatusCode(productoDAO.customError.StatusCode,
                              productoDAO.customError.Message);
        }
        Id = id;
        return Ok();
    }

}
