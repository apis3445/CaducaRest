using System;
using System.IO;
using System.Net.Http;
using CaducaRest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaducaRest.IntegrationTest
{
    public class TestStartup
    {

        private readonly TestServer _server;

        public HttpClient Client { get; }

        public TestStartup()
        {
            var builder = new WebHostBuilder()
                .UseStartup<Startup>()
                .ConfigureAppConfiguration((context, config) =>
                {
                    config.SetBasePath(Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "..\\..\\..\\..\\AspNetCoreTodo"));

                    config.AddJsonFile("appsettings.json");
                })
                .ConfigureServices(services =>
                {
                    services.AddMvcCore(options =>
                    {
                        options.EnableEndpointRouting = false; // TODO: Remove when OData does not causes exceptions anymore
                    });
                    services.AddDbContext<CaducaContext>(opt => opt.UseInMemoryDatabase("Caltic")
                                          .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                })
                ;

            _server = new TestServer(builder);

            Client = _server.CreateClient();
            
        }
    }
}
