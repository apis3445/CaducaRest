using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaducaRest.Models;

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Servicios para los productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly CaducaContext _context;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        public ProductosController(CaducaContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtener todos los productos
        /// </summary>
        /// <returns></returns>
        // GET: api/Productos
        [HttpGet]
        public IEnumerable<Producto> GetProducto()
        {
            return _context.Producto;
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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var producto = await _context.Producto.FindAsync(id);

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

            _context.Entry(producto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

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
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Producto.Add(producto);
            await _context.SaveChangesAsync();

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

            var producto = await _context.Producto.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }

            _context.Producto.Remove(producto);
            await _context.SaveChangesAsync();

            return Ok(producto);
        }

        private bool ProductoExists(int id)
        {
            return _context.Producto.Any(e => e.Id == id);
        }
    }
}