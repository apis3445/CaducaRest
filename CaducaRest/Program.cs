using System;
using CaducaRest.Datos;
using CaducaRest.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CaducaRest;

/// <summary>
/// Clase inicial
/// </summary>
public class Program
{
    /// <summary>
    /// Método de inicio
    /// </summary>
    /// <param name="args">Argumentos</param>
    public static void Main(string[] args)
    {
        var host = CreateHostBuilder(args).Build();
        SeedDatabase(host);
        host.Run();
    }

    /// <summary>
    /// Host Builder para los servicios
    /// </summary>
    /// <param name="args">Argumentos</param>
    /// <returns></returns>
    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)

            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.UseStartup<Startup>();
            });

    private static void SeedDatabase(IHost host)
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<CaducaContext>();
                InicializaDatos.Inicializar(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex.Message);
            }
        }
    }
}
