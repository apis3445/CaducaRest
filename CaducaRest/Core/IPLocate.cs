using System;
using System.Net.Http;
using System.Threading.Tasks;
using CaducaRest.DTO;
using Newtonsoft.Json;

namespace CaducaRest.Core
{
    public class IPLocate
    {
        /// <summary>
        /// LLamamos al servicio IPLocate para obtener los datos de una ip
        /// </summary>
        /// <param name="ip">Ip del cliente</param>
        /// <returns></returns>
        public async Task<DatosIPDTO> ObtenerDatosPorIpAsync(string ip)
        {
            HttpClient client = new HttpClient();
            DatosIPDTO datosIP;
            
            var respuesta = await client.GetStringAsync("https://www.iplocate.io/api/lookup/" + ip);
            datosIP = JsonConvert.DeserializeObject<DatosIPDTO>(respuesta);           
            return datosIP;
        }
    }
}
