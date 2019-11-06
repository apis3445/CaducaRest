using CaducaRest;
using CaducaRest.Datos;
using CaducaRest.DTO;
using CaducaRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnit.CaducaRest
{
    public class UnitTest1
        //: IClassFixture<Prueba<Startup>>
    {

        private  HttpClient httpCliente;

        /*public UnitTest1(Prueba<Startup> factory)
        {
            httpCliente = factory.CreateClient();
        }*/
        [Fact]
        public void SumaDosNumeros_Correcto()
        {
            int a = 1;
            int b=3;
            Operaciones operaciones = new Operaciones(a,b);
            int resultado = operaciones.Sumar();
            Assert.Equal(4, resultado);
        }

        [Fact]
        public async Task Test2Async()
        {
            var builder = new WebHostBuilder()
                            .ConfigureServices(services =>
                            {
                                services.AddMvcCore(options =>
                                {
                                    options.EnableEndpointRouting = false; // TODO: Remove when OData does not causes exceptions anymore
                                });

                                var sp = services.BuildServiceProvider();
                                services.AddDbContext<CaducaContext>(opt => opt.UseInMemoryDatabase("Caltic")
                                                      .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                                // Create a scope to obtain a reference to the database contexts
                                using (var scope = sp.CreateScope())
                                {
                                    var scopedServices = scope.ServiceProvider;
                                    var caducaContext = scopedServices.GetRequiredService<CaducaContext>();
                                    caducaContext.Database.EnsureCreated();
                                    //CaducaRest.Datos.InicializaDatos(caducaContext);
                                    var total = caducaContext.Usuario.ToList();
                                }
                            })
                            .UseStartup<Startup>()

                            .ConfigureAppConfiguration((context, config) =>
                            {
                                config.SetBasePath(Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    "..", "..", "..", "..", "CaducaRest"));
                                config.AddJsonFile("appsettings.json");
                            });
            
            
            var servidorPruebas = new TestServer(builder);

             httpCliente = servidorPruebas.CreateClient();
            
            LoginDTO loginDTO = new LoginDTO();
            loginDTO.Password = "zUvyvsRSCMek58eR";
            loginDTO.Usuario = "Juan";
            var contenido = new StringContent(JsonConvert.SerializeObject(loginDTO), Encoding.UTF8, "application/json");
            var response = await httpCliente.PostAsync("/api/Usuarios/login", contenido);
          
            var codigo = response.StatusCode;
            
             
        }
    }
}
