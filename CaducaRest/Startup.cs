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
using Microsoft.OData.Edm;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Logging;
using GraphQL.Types;
using Microsoft.OData.ModelBuilder;
using Microsoft.AspNetCore.OData;
using GraphQL.Execution;
using GraphQL.MicrosoftDI;
using GraphQL.Server;
using CaducaRest.GraphQL.HotChocolate;
using GraphQL.Utilities;

namespace CaducaRest
{
    /// <summary>
    /// Clase de configuración de servicios
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="env">Hosting Environment</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            CurrentEnvironment = env;
        }
        
        /// <summary>
        /// Acceso al archivo de configuración
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Acceso al enviroment
        /// </summary>
        public IWebHostEnvironment CurrentEnvironment { get; }

        /// <summary>
        /// Permite configurar los servicios de la aplicación
        /// </summary>
        /// <param name="services"></param>
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
                //requieren que los usuarios hayan iniciado sesión
                var policy = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
                options.Filters.Add(typeof(CustomExceptionFilter));
            }).AddJsonOptions(JsonOptions =>
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
            services.AddControllers().AddOData(opt =>
                {
                    opt.Select().Expand().Filter().OrderBy().SetMaxTop(100).Count();
                    opt.AddRouteComponents("odata", GetEdmModel());
                }
            );


            services.AddMvcCore(options =>
            {
                options.EnableEndpointRouting = false;
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
                    services.AddDbContext<CaducaContext>(opt => opt.UseInMemoryDatabase("Caduca")
                                                                            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));
                    break;
                case "IntegrationTesting":
                    var connection = new SqliteConnection("DataSource=:memory:");
                    connection.Open();
                    services.AddDbContext<CaducaContext>(opt => opt.UseSqlite(connection));
                    break;
                default:
                    //services.AddDbContext<CaducaContext>(opt => opt.UseInMemoryDatabase("Caduca")
                    //                                                       .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning)));

                    services.AddDbContext<CaducaContext>(options => options.UseMySql(Configuration.GetConnectionString("DefaultConnection"), ServerVersion.AutoDetect(Configuration.GetConnectionString("DefaultConnection"))));
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
            
            services.AddSingleton<ISchema,CaducidadSchema>();
            services.AddSingleton<CaducidadInputType>();
            services.AddSingleton<CaducidadMutation>();

            services.AddSingleton<ISchema, CaducidadSchema>(services => new CaducidadSchema(new SelfActivatingServiceProvider(services)));
            //services.AddGraphQLServer().AddQueryType<Query>().AddProjections().AddFiltering().AddSorting();
  

            services.AddGraphQL((options, provider) =>
            {
                options.EnableMetrics = CurrentEnvironment.IsDevelopment() ;
                var logger = provider.GetRequiredService<ILogger<Startup>>();
                options.UnhandledExceptionDelegate = ctx => logger.LogError("{Error} occured", ctx.OriginalException.Message);
           }).AddSystemTextJson(deserializerSettings => { }, serializerSettings => { }) // For .NET Core 3+
            .AddErrorInfoProvider(opt => opt.ExposeExceptionStackTrace = CurrentEnvironment.IsDevelopment())
            .AddDataLoader() // Add required services for DataLoader support
            .AddGraphTypes(typeof(CaducidadSchema)); // Add all IGraphType implementors in assembly which ChatSchema exists 
            

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IAuthorizationHandler, PermisoEditHandler>();

        }
        /// <summary>
        /// Permite configurar la aplicación
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="logger"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
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
            var urlAceptadas = Configuration.GetSection("AllowedOrigins").Value.Split(",");
            app.UseCors(builder =>
              builder.WithOrigins(urlAceptadas)
                           .AllowAnyHeader()
                           .AllowAnyMethod());
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //Habilitar swagger
            app.UseSwagger();
            //indica la ruta para generar la configuración de swagger
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("./swagger/v1/swagger.json", "Api Caduca REST");
                c.RoutePrefix = string.Empty;
                c.DefaultModelsExpandDepth(-1);
            });
            
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseGraphQL<ISchema>();
          
            app.UseGraphQLPlayground();
            
            //Indicamos que se van a utilizar varios idiomas
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                //endpoints.MapGraphQL("/graphql");
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

        private void AgregaLog(string mensajeError)
        {
            try
            {
                Correo mail = new Correo(Configuration)
                {
                    Para = "apis3445@gmail.com",
                    Mensaje = mensajeError,
                    Asunto = "Fallo deploy"
                };
                mail.Enviar();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex.Message);
            }
        }
    }
}