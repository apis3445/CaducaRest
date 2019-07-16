using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CaducaRest.Models;
using CaducaRest.DAO;
using System.Collections.Generic;
using CaducaRest.Resources;
using System;
using Microsoft.AspNetCore.Authorization;
using CaducaRest.Core;
using CaducaRest.DTO;

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Servicios para guardar, modificar o borrar las categorías de los productos
    /// </summary>
    [Route("api/[controller]")]   
    [ApiController]
    [ApiVersionNeutral]
    public class CategoriasController : ControllerBase
    {
        private readonly LocService localizacion;
        private readonly CaducaContext _context;
        private CategoriaDAO categoriaDAO;
        private PermisoDTO permisoDTO;
        protected IAuthorizationService _authorizationService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Contexto para la base de datos</param>
        /// <param name="localizer"></param>
        public CategoriasController(CaducaContext context,
                                    LocService localizer,
                                    IAuthorizationService authorizationService)
        {
            _context = context;
            localizacion = localizer;
            _authorizationService = authorizationService;
            categoriaDAO = new CategoriaDAO(context, localizer);
            permisoDTO = new PermisoDTO
            {
                Tabla = "Categoria",
                RequiereAdministrador = false
            };
        }

        /// <summary>
        /// Obtiene todas las categorías registradas
        /// </summary>
        /// <returns>Todas las categorías</returns>
        [HttpGet]
        [Authorize(Roles = "Administrador, Vendedor")]
        public async Task<List<Categoria>> GetCategoriaAsync()
        {
            //Agregamos nuestra validación personalizada
            var authorizationResult = await _authorizationService
                    .AuthorizeAsync(User, permisoDTO, Operaciones.Consultar);
            //Si el resultado no fue exitoso regresamos una lista vacia
            if (!authorizationResult.Succeeded)
                 return new List<Categoria>();
            return await categoriaDAO.ObtenerTodoAsync();
        }

        /// <summary>
        /// Obtiene una categoría de acuerdo a su Id
        /// </summary>
        /// <returns>Los datos de la categoría</returns>
        /// <param name="id">Id de la categoría</param>
        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(Categoria))]
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
        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PutCategoria([FromRoute] int id, [FromBody] Categoria categoria)
        {
            var authorizationResult = await _authorizationService
                   .AuthorizeAsync(User, permisoDTO, Operaciones.Modificar);
            //Si el resultado no fue exitoso regresamos una lista vacia
            if (!authorizationResult.Succeeded)
                return StatusCode(403, String.Format(this.localizacion.GetLocalizedHtmlString("ForbiddenUpdate"), "La categoría"));
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != categoria.Id)
            {
                return BadRequest();
            }

            if (!await categoriaDAO.ModificarAsync(categoria))
            {
                return StatusCode(categoriaDAO.customError.StatusCode,
                                  categoriaDAO.customError.Message);
            }

            return NoContent();
        }

        /// <summary>
        /// Permite registrar una nueva categoría de productos
        /// </summary>
        /// <returns>Los datos de la categoría agregada</returns>
        /// <param name="categoria">Datos de la categoría</param>
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> PostCategoria([FromBody] Categoria categoria)
        {
            var authorizationResult = await _authorizationService
                .AuthorizeAsync(User, permisoDTO, Operaciones.Crear);
            //Si el resultado no fue exitoso regresamos una lista vacia
            if (!authorizationResult.Succeeded)
                return StatusCode(403, String.Format(this.localizacion.GetLocalizedHtmlString("ForbiddenUpdate"), "La categoría"));

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
        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> DeleteCategoria([FromRoute] int id)
        {
            var authorizationResult = await _authorizationService
                        .AuthorizeAsync(User, permisoDTO, Operaciones.Borrar);
            //Si el resultado no fue exitoso regresamos una lista vacia
            if (!authorizationResult.Succeeded)
                return StatusCode(403, String.Format(this.localizacion.
                    GetLocalizedHtmlString("ForbiddenDelete"), "La categoría"));

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (!await categoriaDAO.BorraAsync(id))
            {
                return StatusCode(categoriaDAO.customError.StatusCode,
                                  categoriaDAO.customError.Message);
            }
            return Ok();
        }
    }
}