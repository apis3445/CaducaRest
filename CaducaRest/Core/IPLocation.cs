using System.Threading.Tasks;
using CaducaRest.DTO;

namespace CaducaRest.Core
{
    /// <summary>
    /// Permite obtener los datos de la ip de diferentes proveedores 
    /// </summary>
    public interface IPLocation
    {
        /// <summary>
        /// Obtiene los datos de la ciudad de una ip
        /// </summary>
        /// <param name="ip">Ip del usuario</param>
        /// <returns></returns>
        public Task<DatosIPDTO> ObtenerDatosPorIpAsync(string ip);
    }
}
