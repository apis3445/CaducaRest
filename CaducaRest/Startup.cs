using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using CaducaRest.Core;
using CaducaRest.Filters;
using CaducaRest.GraphQL.Mutation;
using CaducaRest.GraphQL.Schemas;
using CaducaRest.GraphQL.Types;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Diagnostics;
using GraphQL;
using GraphQL.Server;
using GraphQL.Server.Ui.Playground;
using Microsoft.AspNet.OData.Extensions;
using System.Linq;
using Microsoft.AspNet.OData.Formatter;
using System.Net.Http.Headers;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using System.Text.Json;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CaducaRest
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment CurrentEnvironment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            var urlsPermitidas = Configuration.GetSection("AllowedHosts").Value;
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
            services.AddControllers(options =>
            {
                options.EnableEndpointRouting = false;
                //Agregamos una politica para indicar que nuestros servicios 
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
                        new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/prs.mock-odata"));
                }
                foreach (var formatter in options.InputFormatters
                    .OfType<ODataInputFormatter>()
                    .Where(it => !it.SupportedMediaTypes.Any()))
                {
                    formatter.SupportedMediaTypes.Add(
                        new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/prs.mock-odata"));
                }
                
            }
                ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddJsonOptions(JsonOptions =>
                    JsonOptions.JsonSerializerOptions.PropertyNamingPolicy = null)
            .AddDataAnnotationsLocalization(options =>
            {
                //Indicamos que el modelo tomara los mensajes de error del archivo SharedResource
                options.DataAnnotationLocalizerProvider = (type, factory) =>
                {
                    var assemblyName = new AssemblyName(typeof(SharedResource).GetTypeInfo().Assembly.FullName);
                    return factory.Create("SharedResource", assemblyName.Name);
                };
            });
            //services.AddApiVersioning(options => options.ReportApiVersions = true);
            services.AddOData();

            services.AddMvcCore(options =>
            {
                foreach (var outputFormatter in options.OutputFormatters.OfType<ODataOutputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    outputFormatter.SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
                foreach (var inputFormatter in options.InputFormatters.OfType<ODataInputFormatter>().Where(_ => _.SupportedMediaTypes.Count == 0))
                {
                    inputFormatter.SupportedMediaTypes.Add(new Microsoft.Net.Http.Headers.MediaTypeHeaderValue("application/prs.odatatestxx-odata"));
                }
            });
            //La clase LocService nos permite cambiar los mensajes de error según el idioma
            services.AddSingleton<LocService>();
            //Le indicamos la carpeta donde estan los mensajes que utiliza la aplicación
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
            switch (CurrentEnvironment.EnvironmentName)
            {
                case "Testing":
                    //Conexión en Memoria
                    services.AddDbContext<CaducaContext>(opt => opt.UseInMemoryDatabase("Caltic")
                                                                            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                    break;
                case "IntegrationTesting":
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();
                    services.AddDbContext<CaducaContext>(opt => opt.UseSqlite(connection));
                    break;
                default:
                    
                    services.AddDbContext<CaducaContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection")));
                    //Conexión SQL Server
                    //services.AddDbContext<CaducaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("SQLServerConnection")));
                    //Conexión SQL Server Azure
                    //services.AddDbContext<CaducaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("AzureSQLConnection")));
                    break;
            }

            //Habilitar CORS
            services.AddCors();
            
            //Se agrega en generador de Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api Caduca REST", Version = "v1" });
                //Obtenemos el nombre de la dll por medio de reflexión
                var assemblyName = Assembly.GetEntryAssembly().GetName().Name;
                if (assemblyName != "testhost")
                {
                    //Al nombre del assembly le agregamos la extensión xml
                    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    //Agregamos el Path, es importante utilizar el comando Path.Combine
                    //ya que entre windows y linux cambian las rutas de los archivos
                    //En windows es por ejemplo c:/Uusuarios con / y en linux es \usr
                    // con \
                    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                    c.IncludeXmlComments(xmlPath);
                }
                
                c.AddSecurityDefinition("Bearer", 
                new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization (bearer)",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer" 
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement{
                    {
                        new OpenApiSecurityScheme{
                            Reference = new OpenApiReference{
                                Id = "Bearer", 
                                Type = ReferenceType.SecurityScheme
                            }
                        },new List<string>()
                    }
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
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsEnvironment("Testing"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            var urlAceptadas = Configuration.GetSection("AllowedHosts").Value.Split(",");
            app.UseCors(builder =>
              builder.WithOrigins(urlAceptadas)
                           .AllowAnyHeader()
                           .AllowAnyMethod()
                           );
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Habilitar swagger
            app.UseSwagger();
            //indica la ruta para generar la configuración de swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Caduca REST");
                c.RoutePrefix = string.Empty;
            });
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGraphQL<CaducidadSchema>();
          
            app.UseGraphQLPlayground(options: new GraphQLPlaygroundOptions());

            //Indicamos que se van a utilizar varios idiomas
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);
            
            /*app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
            
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Select().Expand().Filter().OrderBy().MaxTop(100).Count();
                routeBuilder.MapODataServiceRoute("odata", "odata", GetEdmModel());
                routeBuilder.EnableDependencyInjection();
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
