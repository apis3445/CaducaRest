using System;
using System.Threading.Tasks;
using CaducaRest.DTO;

namespace CaducaRest.Core
{
    public interface IPLocation
    {
        public Task<DatosIPDTO> ObtenerDatosPorIpAsync(string ip);
    }
}
