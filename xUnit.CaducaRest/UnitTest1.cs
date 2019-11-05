using CaducaRest;
using CaducaRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnit.CaducaRest
{
    public class UnitTest1
    {

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
        public void Test2()
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
            var servidorPruebas = new TestServer(builder);

            var httpCliente = servidorPruebas.CreateClient();
        }
    }
}
