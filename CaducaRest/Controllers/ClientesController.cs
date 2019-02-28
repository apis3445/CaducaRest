using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaducaRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ODataController
    {
        private readonly LocService _localizer;
        private readonly CaducaContext _context;

        public ClientesController(CaducaContext context, LocService localizer)
        {
            _context = context;
            _localizer = localizer;
            
        }

        [EnableQuery]
        public IActionResult Get()
        {
            var clientes = _context.Cliente;
            return Ok(clientes);
        }

        [EnableQuery]
        public IActionResult Get([FromODataUri]int key)
        {
            return Ok(_context.Cliente.Find(key));
        }
    }
}