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
        private readonly TestServer servidorPruebas;

        public HttpClient httpCliente { get; }
        public HttpResponseMessage httpResponse;
        public Servicios()
        {
            var contexto = new CaducaContextMemoria().ObtenerContexto();
            var builder = new WebHostBuilder()
                            .ConfigureServices(services =>
                            {
                                services.AddMvcCore(options =>
                                {
                                    options.EnableEndpointRouting = false; // TODO: Remove when OData does not causes exceptions anymore
                                });
                                services.AddDbContext<CaducaContext>(opt => opt.UseInMemoryDatabase("Caltic")
                                                      .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                            })
                            .UseStartup<Startup>()
                            
                            .ConfigureAppConfiguration((context, config) =>
                            {
                                config.SetBasePath(Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    "..", "..", "..", "..", "CaducaRest"));
                                config.AddJsonFile("appsettings.json");
                            });
            servidorPruebas = new TestServer(builder);

            httpCliente = servidorPruebas.CreateClient();
           
        }

        public async Task<bool> PostAsync(string servicio, object datos)
        {
            var contenido = new StringContent(JsonConvert.SerializeObject(datos), Encoding.UTF8, "application/json");
            var response = await httpCliente.PostAsync(Constantes.URLBase + servicio, contenido);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}
