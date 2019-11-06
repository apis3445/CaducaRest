using System;
using CaducaRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Linq;
using CaducaRest;

namespace xUnit.CaducaRest
{
    public class Prueba<TStartup> : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                var serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkInMemoryDatabase()
                    .BuildServiceProvider();

                // Add a database context (AppDbContext) using an in-memory database for testing.
                services.AddDbContext<CaducaContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryAppDb");
                    options.UseInternalServiceProvider(serviceProvider);
                });
                services.AddMvcCore(options =>
                {
                    options.EnableEndpointRouting = false; // TODO: Remove when OData does not causes exceptions anymore
                });
                services.AddLogging(b => b
                       .AddConsole()
                       .AddFilter(level => level >= LogLevel.Trace)
                   );
                // Build the service provider.
                var sp = services.BuildServiceProvider();

                // Create a scope to obtain a reference to the database contexts
                using (var scope = sp.CreateScope())
                {
                    var scopedServices = scope.ServiceProvider;
                    var appDb = scopedServices.GetRequiredService<CaducaContext>();

                    var logger = scopedServices.GetRequiredService<ILogger<Prueba<TStartup>>>();

                    // Ensure the database is created.
                    appDb.Database.EnsureCreated();
                    var loggerFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();
                    
                    var total = appDb.Usuario.ToList();
                }
            });
        }
    }
}
