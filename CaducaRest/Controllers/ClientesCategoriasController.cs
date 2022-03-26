using System.Linq;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Controller Odata para clientes categorias
    /// </summary>
    public class ClientesCategoriasController : ODataController
    {
        private readonly LocService _localizer;
        private readonly CaducaContext _context;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="localizer"></param>
        public ClientesCategoriasController(CaducaContext context, LocService localizer)
        {
            _context = context;
            _localizer = localizer;
           
        }

        /// <summary>
        /// Regresa las categorias de productos que un cliente tiene acceso
        /// </summary>
        /// <returns></returns>
        [EnableQuery(PageSize = 10,
                   AllowedQueryOptions = AllowedQueryOptions.Skip |
                                         AllowedQueryOptions.Top |
                                         AllowedQueryOptions.Count |
                                         AllowedQueryOptions.Select |
                                         AllowedQueryOptions.Expand)]
        public IQueryable<ClienteCategoria> Get()
        {            
            return _context.ClienteCategoria;
        }

        /// <summary>
        /// Regresa las categorias de productos que un cliente tiene acceso por Id
        /// </summary>
        /// <param name="key">Id de Cliente Categoria</param>
        /// <returns></returns>
        [HttpGet("{key}")]
        [EnableQuery]
        public ActionResult<IQueryable<ClienteCategoria>> GetClienteCategoria(int key)
        {
            
            return Ok(_context.ClienteCategoria.Where(c=>c.Id== key));
        }


    }
}
