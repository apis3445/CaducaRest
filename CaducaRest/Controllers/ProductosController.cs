using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CaducaRest.Models;
using CaducaRest.Resources;
using CaducaRest.DAO;
using Microsoft.AspNetCore.Authorization;

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Servicios para los productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersionNeutral]
    [Authorize(Roles = "Administrador")]
    public class ProductosController : ControllerBase
    {
        private readonly LocService _localizer;
        private readonly CaducaContext _context;
        private ProductoDAO productoDAO;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="localize"></param>
        public ProductosController(CaducaContext context, LocService localize)
        {
            _context = context;
            productoDAO = new ProductoDAO(context, localize);
        }

        /// <summary>
        /// Obtener todos los productos
        /// </summary>
        /// <returns></returns>
        // GET: api/Productos
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

            return CreatedAtAction("GetCategoria", new { id = producto.Id }, producto);
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
            return Ok();
        }

    }
}