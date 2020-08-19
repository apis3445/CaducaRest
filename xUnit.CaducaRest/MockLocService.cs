using System;
using System.IO;
using CaducaRest;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaducaRest.UnitTest
{
    public class MockLocService
    {

        public LocService ObtenerLocService()
        {
            var builder = new WebHostBuilder()
                            .UseStartup<Startup>()
                            .ConfigureAppConfiguration((context, config) =>
                            {
                                config.SetBasePath(Path.Combine(
                                    Directory.GetCurrentDirectory(),
                                    "..", "..", "..", "..", "CaducaRest"));
                                config.AddJsonFile("appsettings.json");
                            });
            var scope = builder.Build().Services.CreateScope();
            return scope.ServiceProvider
                                .GetRequiredService<LocService>();
        }

    }
}
