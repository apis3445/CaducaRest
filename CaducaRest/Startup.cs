using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

namespace CaducaRest
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            //LA clase LocServicde nos permite cambiar los mensajes de error según el idioma
            services.AddSingleton<LocService>();
            //Le indicamos la carpeta donde estan todos los mensajes que utiliza la aplicación
            services.AddLocalization(options => options.ResourcesPath = "Resources");
            //Indicamos que el modelo tomara los mensajes de error del archivo SharedResource
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(JsonOptions => JsonOptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddDataAnnotationsLocalization(options =>
                {
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });
            //Agregamos los idiomas que tiene configurada nuestra aplicación es esta caso es español e ingles
            //Seleccionamos por default es-MX
            services.Configure<RequestLocalizationOptions>(
                options =>
                {
                    var supportedCultures = new List<CultureInfo>
                        {
                            new CultureInfo("es-MX"),
                            new CultureInfo("en-US"),
                        };

                    options.DefaultRequestCulture = new RequestCulture(culture: "es-MX", uiCulture: "es-MX");
                    options.SupportedCultures = supportedCultures;
                    options.SupportedUICultures = supportedCultures;

                    options.RequestCultureProviders.Insert(0, new QueryStringRequestCultureProvider());
                });
            services.AddDbContext<CaducaContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            //Se agrega en generador de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Api Caduca REST", Version = "v1" });
                var basePath = AppContext.BaseDirectory;
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                var fileName = System.IO.Path.GetFileName(assemblyName + ".xml");
                var xmlPath = Path.Combine(basePath, fileName);
                c.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Habilitar swagger
            app.UseSwagger();
            //Indicamos que se van a utilizar varios idiomas
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            //indica la ruta para generar la configuración de swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Caduca REST");
            });
            app.UseMvc();
        }
    }
}
