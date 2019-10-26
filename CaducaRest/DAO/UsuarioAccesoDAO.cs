using System;
using System.Linq;
using System.Threading.Tasks;
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

    public DatosIPDTO datosIP;
        
    public UsuarioAccesoDAO(CaducaContext context, LocService locService)
    {
        this.contexto = context;
        this.localizacion = locService;
    }

    public async Task<DatosIPDTO> ObtenerDatosIPAsync(string ip)
    {
        var ipLocate = new IPLocate();
        return  await ipLocate.ObtenerDatosPorIpAsync(ip);           
    }

    public async Task<bool> EsOtraCiudadAsync(string ip, int usuarioId)
    {
        //Obtenemos los datos de la ip
        datosIP = await this.ObtenerDatosIPAsync(ip);
        //Revisamos si ya registro algún acceso, si no tiene
        //accesos no se debe enviar ningún correo 
        var acceso = contexto.UsuarioAcceso.FirstOrDefault(u => u.UsuarioId == usuarioId);
        if (acceso == null)
            return false;
        //Revisamos si tiene un registro en otra ciduad
        acceso = contexto.UsuarioAcceso.FirstOrDefault(u => u.UsuarioId == usuarioId
                                                && u.Ciudad!=datosIP.city);
        //Si ya tiene un acceso para esa ciudad regresamos false
        return acceso!=null;
    }

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

        public async Task<bool> GuardarAccesoAsync(TokenDTO tokenDTO, int usuarioId, string ip, string navegador)
        {           
            if (datosIP == null)
                datosIP = await ObtenerDatosIPAsync(ip);
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
