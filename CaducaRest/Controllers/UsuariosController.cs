﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CaducaRest.Core;
using CaducaRest.DAO;
using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace CaducaRest.Controllers
{
    [Route("api/[controller]")]
    public class UsuariosController : BaseController
    {
        private UsuarioDAO usuarioDAO;

        private IHttpContextAccessor _accessor;

        protected readonly IConfiguration _config;

        private readonly IHostingEnvironment _hostingEnvironment;

        public UsuariosController(CaducaContext context,
                                  LocService localize,
                                  IConfiguration config,
                                  IHostingEnvironment hostingEnvironment,
                                  IHttpContextAccessor accessor) : base(context, localize)
        {           
            _config = config;
            _accessor = accessor;
            _hostingEnvironment = hostingEnvironment;
            usuarioDAO = new UsuarioDAO(context, localize, _hostingEnvironment.ContentRootPath);
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
            var navegador = _accessor.HttpContext?.Request.Headers["User-Agent"];
            //Para obtener cualquier otro dato en el Header
            //var otroDato = _accessor.HttpContext?.Request.Headers["Secreto"];
            string ip = "198.27.75.143"; //Set default ip
            if (_hostingEnvironment.IsProduction())        
                ip =_accessor.HttpContext?.Connection?.RemoteIpAddress?.ToString();
            var token = await usuarioDAO.LoginAsync(loginDTO, _config, ip, navegador);
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

        [HttpPost("Refresh")]
        [AllowAnonymous]
        public IActionResult RefreshToken([FromBody]RefreshTokenDTO refreshToken)
        {
            if (!usuarioDAO.ValidarToken(refreshToken.RefreshToken, _config))
                return StatusCode(403, new CustomError(403, this._localizer.GetLocalizedHtmlString("AccesoNoAutorizado")));
            return Ok(usuarioDAO.tokenDTO);
        }
    }
}