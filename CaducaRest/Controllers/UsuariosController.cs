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
using Microsoft.Extensions.Hosting;

namespace CaducaRest.Controllers
{
    /// <summary>
    /// Servicios para usuarios y login
    /// </summary>
    [Route("api/[controller]")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class UsuariosController : BaseController
    {
        private UsuarioDAO usuarioDAO;

        private IHttpContextAccessor _accessor;

        /// <summary>
        /// Archivo de configuración
        /// </summary>
        protected readonly IConfiguration _config;

        private readonly IWebHostEnvironment _hostingEnvironment;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">Base de datos</param>
        /// <param name="localize">Idiomas</param>
        /// <param name="config">Archivo de configuración</param>
        /// <param name="hostingEnvironment">Environment</param>
        /// <param name="accessor"></param>
        public UsuariosController(CaducaContext context,
                                  LocService localize,
                                  IConfiguration config,
                                  IWebHostEnvironment hostingEnvironment,
                                  IHttpContextAccessor accessor) : base(context, localize)
        {           
            _config = config;
            _accessor = accessor;
            _hostingEnvironment = hostingEnvironment;
            usuarioDAO = new UsuarioDAO(context, localize, _hostingEnvironment.ContentRootPath);
        }

        /// <summary>
        /// Login para los usuarios
        /// </summary>
        /// <param name="loginDTO">Datos del usuario</param>
        /// <returns></returns>
        [HttpPost("Login")]
        [ProducesResponseType(typeof(TokenDTO), StatusCodes.Status200OK)]
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

        /// <summary>
        /// Permite refrescar el token
        /// </summary>
        /// <param name="refreshToken">Datos para refrescar el token</param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        [AllowAnonymous]
        public IActionResult RefreshToken([FromBody]RefreshTokenDTO refreshToken)
        {
            if (!usuarioDAO.ValidarToken(refreshToken.RefreshToken, _config))
                return StatusCode(403, new CustomError(403, this._localizer.GetLocalizedHtmlString("AccesoNoAutorizado")));
            return Ok(usuarioDAO.tokenDTO);
        }
    }
}