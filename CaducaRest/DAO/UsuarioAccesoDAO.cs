using System;
using System.Linq;
using System.Threading.Tasks;
using CaducaRest.Core;
using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.Resources;

namespace CaducaRest.DAO
{
    /// <summary>
    /// Funciones para accesos a datos para registar los accesos del
    /// usuario al sistema
    /// </summary>
    public class UsuarioAccesoDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        public DatosIPDTO datosIP;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="locService"></param>
        public UsuarioAccesoDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
        }

        /// <summary>
        /// Obtiene los datos de una ip
        /// </summary>
        /// <param name="ip">Ip de la cual se realiza la petición</param>
        /// <param name="ipLocation">Clase que se conecta al servicio rest
        /// para obtener los datos de la ip</param>
        /// <returns></returns>
        public async Task<DatosIPDTO> ObtenerDatosIPAsync(string ip, IPLocation ipLocation)
        {
            return await ipLocation.ObtenerDatosPorIpAsync(ip);
        }

        /// <summary>
        /// Revisa si la ip del usuario pertenece a una ciudad diferente
        /// al historial de ciudades de donde se conecta el usuario
        /// </summary>
        /// <param name="ip">Ip de la cual se conecta el usuario</param>
        /// <param name="usuarioId">Id del usuario</param>
        /// <returns></returns>
        public async Task<bool> EsOtraCiudadAsync(string ip, int usuarioId)
        {
            IPGeoLocation ipGeoLocation = new IPGeoLocation();
            //Obtenemos los datos de la ip

            datosIP = await this.ObtenerDatosIPAsync(ip, ipGeoLocation);
            //Revisamos si ya registro algún acceso, si no tiene
            //accesos no se debe enviar ningún correo 
            var acceso = contexto.UsuarioAcceso.FirstOrDefault(u => u.UsuarioId == usuarioId);
            if (acceso == null)
                return false;
            //Revisamos si tiene un registro en otra ciduad
            acceso = contexto.UsuarioAcceso.FirstOrDefault(u => u.UsuarioId == usuarioId
                                                    && u.Ciudad != datosIP.city);
            //Si ya tiene un acceso para esa ciudad regresamos false
            return acceso != null;
        }

        /// <summary>
        /// Revisa si el usuario se conecta de un navegador diferente
        /// </summary>
        /// <param name="navegador">Nombre del navegador del cual se conecta
        /// el usuario</param>
        /// <param name="usuarioId">Id del usuario</param>
        /// <returns></returns>
        public bool EsOtroNavegador(string navegador, int usuarioId)
        {

            //Revisamos si ya registro algún acceso, si no tiene
            //accesos no se debe enviar ningún correo 
            var acceso = contexto.UsuarioAcceso.FirstOrDefault(u => u.UsuarioId == usuarioId);
            if (acceso == null)
                return false;
            //Revisamos si tiene un registro en otra ciduad
            acceso = contexto.UsuarioAcceso.FirstOrDefault(u => u.UsuarioId == usuarioId
                                                    && u.Navegador != navegador);
            //Si ya tiene un acceso para esa ciudad regresamos false
            return acceso != null;
        }

        /// <summary>
        /// Guarda los datos del acceso del usuario
        /// </summary>
        /// <param name="tokenDTO">Datos del token</param>
        /// <param name="usuarioId">Id del usuario</param>
        /// <param name="ip">Ip de la cual se conecta el usuario</param>
        /// <param name="navegador">Navegador del cual se conecta el usuario</param>
        /// <returns></returns>
        public async Task<bool> GuardarAccesoAsync(TokenDTO tokenDTO, int usuarioId, string ip, string navegador)
        {
            IPGeoLocation ipGeoLocation = new IPGeoLocation();
            if (datosIP == null)
                datosIP = await ObtenerDatosIPAsync(ip, ipGeoLocation);
            var usuarioAcceso = new UsuarioAcceso();
            usuarioAcceso.Ciudad = datosIP.city;
            usuarioAcceso.Estado = datosIP.subdivision;
            usuarioAcceso.Navegador = navegador;
            usuarioAcceso.UsuarioId = usuarioId;
            usuarioAcceso.Fecha = DateTime.Now;
            usuarioAcceso.Token = tokenDTO.Token;
            usuarioAcceso.Activo = true;
            usuarioAcceso.SistemaOperativo = "Default";
            usuarioAcceso.RefreshToken = tokenDTO.RefreshToken;
            usuarioAcceso.Navegador = "Default";
            contexto.UsuarioAcceso.Add(usuarioAcceso);
            contexto.SaveChanges();
            return true;
        }
    }
}
