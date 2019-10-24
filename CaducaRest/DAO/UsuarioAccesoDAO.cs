using System;
using CaducaRest.Core;
using CaducaRest.DTO;
using CaducaRest.Models;
using CaducaRest.Resources;

namespace CaducaRest.DAO
{
    public class UsuarioAccesoDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;


        public UsuarioAccesoDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
        }

        public async System.Threading.Tasks.Task<bool> AgregarAsync(TokenDTO tokenDTO, int usuarioId, string ip)
        {
            var usuarioAcceso = new UsuarioAcceso();

            var ipLocate = new IPLocate();
            var datosIP = await ipLocate.ObtenerDatosPorIpAsync(ip);
            if (datosIP != null)
            {
                usuarioAcceso.Ciudad = datosIP.city;
                usuarioAcceso.Estado = datosIP.subdivision;
            }
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
