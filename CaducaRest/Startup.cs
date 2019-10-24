using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CaducaRest.Core;
using CaducaRest.Filters;
using CaducaRest.GraphQL.Mutation;
using CaducaRest.GraphQL.Schemas;
using CaducaRest.GraphQL.Types;
using CaducaRest.Models;
using CaducaRest.Resources;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OData.Edm;
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
            services.AddOData();
            //La clase LocServicde nos permite cambiar los mensajes de error según el idioma
            services.AddSingleton<LocService>();
            //Le indicamos la carpeta donde estan todos los mensajes que utiliza la aplicación
            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddAuthentication(o => {
                            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    })
                    .AddJwtBearer(cfg =>
                    {
                        cfg.Audience = Configuration["Tokens:Audience"];
                        cfg.ClaimsIssuer = Configuration["Tokens:Issuer"];
                        cfg.TokenValidationParameters = new TokenValidationParameters()
                        {
                            ValidIssuer = Configuration["Tokens:Issuer"],
                            ValidateAudience = true,
                            ValidAudience = Configuration["Tokens:Audience"],
                            //Se valida la llave de cifrado
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Tokens:Key"])),
                            //Se validara el tiempo de vida del token
                            ValidateLifetime = true
                        };
                    });
            services.AddMvc(options =>
                        {
                            //Agregamos una politica para indicar que todos nuestros servicios 
                            //requieren que los usuarios hayan iniciado sesióm
                            var policy = new AuthorizationPolicyBuilder()
                                                .RequireAuthenticatedUser()
                                                .Build();
                            options.Filters.Add(new AuthorizeFilter(policy));
                            
                            
                            options.Filters.Add(typeof(CustomExceptionFilter));
                            foreach (var formatter in options.OutputFormatters
                                        .OfType<ODataOutputFormatter>()
                                        .Where(it => !it.SupportedMediaTypes.Any()))
                            {
                                formatter.SupportedMediaTypes.Add(
                                    new MediaTypeHeaderValue("application/prs.mock-odata"));
                            }
                            foreach (var formatter in options.InputFormatters
                                .OfType<ODataInputFormatter>()
                                .Where(it => !it.SupportedMediaTypes.Any()))
                            {
                                formatter.SupportedMediaTypes.Add(
                                    new MediaTypeHeaderValue("application/prs.mock-odata"));
                            }
                        }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(JsonOptions => JsonOptions.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver())
                .AddDataAnnotationsLocalization(options =>
                {
                    //Indicamos que el modelo tomara los mensajes de error del archivo SharedResource
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                    {
                        var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                        return factory.Create("SharedResource", assemblyName.Name);
                    };
                });


            services.AddAuthorization(options =>
            {
               options.AddPolicy("VendedorConCategorias", policy => policy.RequireClaim("Categorias"));
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
            //Conexión MySQL
            services.AddDbContext<CaducaContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
            //Conexión SQL Server
            //services.AddDbContext<CaducaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));
            //Conexión SQL Server Azure
            //services.AddDbContext<CaducaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureSQLConnection")));
            //Se agrega en generador de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Api Caduca REST", Version = "v1" });
                //Obtenemos el directorio actual
                var basePath = AppContext.BaseDirectory;
                //Obtenemos el nombre de la dll por medio de reflexión
                var assemblyName = System.Reflection.Assembly.GetEntryAssembly().GetName().Name;
                //Al nombre del assembly le agregamos la extensión xml
                var fileName = System.IO.Path.GetFileName(assemblyName + ".xml");
                //Agregamos el Path, es importante utilizar el comando Path.Combine
                //ya que entre windows y mas cambian las rutas de los archivos
                //En windows es por ejemplo c:/Uusuarios con / y en linux es \usr
                // con \
                var xmlPath = Path.Combine(basePath, fileName);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Bearer", new ApiKeyScheme()
                {
                    In = "header",
                    Description = "Por favor teclea el header de autorización",
                    Name = "Authorization",
                    Type = "apiKey"
                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>>
                    {
                        { "Bearer", new string[] { } }
                    });
            });

            services.AddScoped<IDependencyResolver>(s =>
                new FuncDependencyResolver(s.GetRequiredService));
            services.AddScoped<CaducidadSchema>();
            services.AddScoped<CaducidadInputType>();
            services.AddScoped<CaducidadMutation>();
            services.AddGraphQL(x =>
            {
                x.ExposeExceptions = true; //set true only in development mode. make it switchable.
            })
            .AddGraphTypes(ServiceLifetime.Scoped)
            .AddUserContextBuilder(httpContext => httpContext.User)
            .AddDataLoader();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthorizationHandler, PermisoEditHandler>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseGraphQL<CaducidadSchema>();
          
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

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
            app.UseMvc(b =>
            {
                b.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                b.MapODataServiceRoute("odata", "odata", GetEdmModel());
            });
        }

        private static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
            builder.EntitySet<Cliente>("Clientes");
            builder.EntitySet<ClienteCategoria>("ClientesCategorias");
            builder.EntitySet<Caducidad>("Caducidad");
            return builder.GetEdmModel();
        }
    }
}
