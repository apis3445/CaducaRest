using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using CaducaRest.DTO;

namespace CaducaRest.Core
{
    /// <summary>
    /// Obtiene los datos de una ip de IPLocate
    /// </summary>
    public class IPLocate: IPLocation
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
            datosIP = JsonSerializer.Deserialize<DatosIPDTO>(respuesta);           
            return datosIP;
        }
    }
}
