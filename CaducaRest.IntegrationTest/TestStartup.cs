using System;
using CaducaRest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaducaRest.IntegrationTest
{
    public class TestStartup : Startup
    {
        
        public TestStartup(IHostingEnvironment env, IConfiguration configuration) : base(configuration)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(env.ContentRootPath)
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
               .AddEnvironmentVariables();
            configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureTestServices(IServiceCollection services)
        {
           services.AddDbContext<CaducaContext>
                (opt => opt.UseInMemoryDatabase("Caltic")
                .ConfigureWarnings(x =>
                x.Ignore(InMemoryEventId
                       .TransactionIgnoredWarning)));
            
        }

        public void Configure(IApplicationBuilder app)
        {
            // your usual registrations there
        }
    }
}
