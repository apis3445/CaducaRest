using System;
using CaducaRest.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaducaRest.IntegrationTest
{
    public class TestStartup : Startup
    {
        public TestStartup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureTestServices(IServiceCollection services)
        {
            services.
                .AddDbContext<CaducaContext>(
                    
                );
        }

        public void Configure(IApplicationBuilder app)
        {
            // your usual registrations there
        }
    }
}
