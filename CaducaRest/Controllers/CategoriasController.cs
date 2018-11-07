using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaducaRest.Models;
using CaducaRest.DAO;
using System.Collections.Generic;

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Servicios para guardar, modificar o borrar las categorías de los productos
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly CaducaContext _context;
        private CategoriaDAO categoriaDAO;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Contexto para la base de datos</param>
        public CategoriasController(CaducaContext context)
        {
            _context = context;
            categoriaDAO = new CategoriaDAO(_context);
        }

        /// <summary>
        /// Obtiene todas las categorías registradas
        /// </summary>
        /// <returns>Todas las categorías</returns>
        [HttpGet]
        public List<Categoria> GetCategoria()
        {           
            return categoriaDAO.ObtenerTodo();
        }

        /// <summary>
        /// Obtiene una categoría de acuerdo a su Id
        /// </summary>
        /// <returns>Los datos de la categoría</returns>
        /// <param name="id">Id de la categoría</param>
        // GET: api/Categorias/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoria([FromRoute] int id)
        {

            var categoria = await categoriaDAO.ObtenerPorIdAsync(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return Ok(categoria);
        }

        /// <summary>
        /// Modifica una categoría
        /// </summary>
        /// <returns>No Content si se modifico correctamente</returns>
        /// <param name="id">Id de la categoría a Modificar</param>
        /// <param name="categoria">Datos de la Categoria.</param>
        // PUT: api/Categorias/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria([FromRoute] int id, [FromBody] Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.Id)
            {
                return BadRequest();
            }

            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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
        /// Permite registrar una nueva categoría de productos
        /// </summary>
        /// <returns>Los datos de la categoría agregada</returns>
        /// <param name="categoria">Datos de la categoría</param>

        [HttpPost]
        public async Task<IActionResult> PostCategoria([FromBody] Categoria categoria)
        {
            if (!await categoriaDAO.AgregarAsync(categoria))
            {
                return StatusCode(categoriaDAO.customError.StatusCode, 
                                  categoriaDAO.customError.Message);
            }

            return CreatedAtAction("GetCategoria", new { id = categoria.Id }, categoria);
        }

        /// <summary>
        /// Permite borrar una categoría
        /// </summary>
        /// <returns>Los datos de la categoría eliminada</returns>
        /// <param name="id">Id de la categoría a borrar</param>
        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoria([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categoria = await _context.Categoria.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            _context.Categoria.Remove(categoria);
            await _context.SaveChangesAsync();

            return Ok(categoria);
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categoria.Any(e => e.Id == id);
        }
    }
}