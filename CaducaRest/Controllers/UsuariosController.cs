using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CaducaRest.DAO;
using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CaducaRest.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : ControllerBase
    {
        private UsuarioDAO usuarioDAO;
        protected readonly IConfiguration _config;

        public UsuariosController(CaducaContext context, LocService localize, IConfiguration config)
        {
            usuarioDAO = new UsuarioDAO(context, localize);
            _config = config;
        }

        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new List<string>();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

    [HttpPost("Login")]
    [AllowAnonymous]
    public async Task<IActionResult> PostAsync([FromBody] LoginDTO loginDTO)
    {
        var token = await usuarioDAO.LoginAsync(loginDTO, _config);
        if (string.IsNullOrEmpty(token.Token))         
            return StatusCode(usuarioDAO.customError.StatusCode, usuarioDAO.customError.Message);
        return Ok(token);
    }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
