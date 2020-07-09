using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CaducaRest.Controllers
{
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

        [HttpGet("{key}")]
        [EnableQuery]
        public ActionResult<IQueryable<ClienteCategoria>> GetClienteCategoria(int key)
        {
            
            return Ok(_context.ClienteCategoria.Where(c=>c.Id== key));
        }


    }
}
