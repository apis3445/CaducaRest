using CaducaRest.Datos;
using CaducaRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CaducaRest.IntegrationTest
{
    public class Servicios
    {
        public static HttpClient httpCliente;
        private static CaducaContext caducaContext;
        public HttpResponseMessage httpResponse;
       
        
       
                public static void Inicializa()
                {
                    var builder = new WebHostBuilder()
                         .UseEnvironment("Testing")
                         .ConfigureAppConfiguration((c, config) =>
                         {
                             config.SetBasePath(Path.Combine(
                                 Directory.GetCurrentDirectory(),
                                 "..", "..", "..", "..", "CaducaRest"));

                             config.AddJsonFile("appsettings.json");
                         })
                         .UseStartup<Startup>();
                    var servidorPruebas = new TestServer(builder);
                    caducaContext = servidorPruebas.Host.Services.GetService(typeof(CaducaContext)) as CaducaContext;
                    httpCliente = servidorPruebas.CreateClient();
                    InicializaDatos.Inicializar(caducaContext);
           
                }

        public async Task<bool> PostAsync(string servicio, object datos)
        {
            var contenido = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
            var response = await httpCliente.PostAsync(servicio, contenido);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}
