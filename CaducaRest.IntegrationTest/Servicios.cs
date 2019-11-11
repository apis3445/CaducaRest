using CaducaRest.Datos;
using CaducaRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CaducaRest.IntegrationTest
{
    public class Servicios
    {
        private static HttpClient httpCliente;
        public static CaducaContext caducaContext;

        public static void Inicializa()
        {
            //Creamos un servidor de pruebas utilizando un ambiente
            //de testing
            var builder = new WebHostBuilder()
                    .UseEnvironment("IntegrationTesting")
                    .ConfigureAppConfiguration((c, config) =>
                    {
                        config.SetBasePath(Path.Combine(
                            Directory.GetCurrentDirectory(),
                            "..", "..", "..", "..", "CaducaRest"));

                        config.AddJsonFile("appsettings.json");
                    })
                    .UseStartup<Startup>();
            //Esto nos crea un servidor con los servicios rest 
            var servidorPruebas = new TestServer(builder);
            //Obtenemos el objeto caducaContext
            caducaContext = servidorPruebas.Host.Services.GetService(typeof(CaducaContext)) as CaducaContext;
            //Abrimos la conexión
           
            //Inicializamos la bd con datos de prueba
            InicializaDatos.Inicializar(caducaContext);
            //var total =caducaContext.Usuario.Count();
            httpCliente = servidorPruebas.CreateClient();
        }

        public static async Task<bool> PostAsync(string servicio, object datos)
        {
            var contenido = new StringContent(JsonSerializer.Serialize(datos), Encoding.UTF8, "application/json");
            var response = await httpCliente.PostAsync("api/" + servicio, contenido);
            if (response.StatusCode == HttpStatusCode.OK)
                return true;
            return false;
        }
    }
}