using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

namespace Csla8ModelTemplates.WebApi.Extensions
{
    /// <summary>
    /// Provide methods to configure Swagger.
    /// </summary>
    internal static class SwaggerExtensions
    {
        /// <summary>
        /// Configures the Swagger generator.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="environment">The web hosting environment the application is running in.</param>
        public static void Add_Swagger(
            this IServiceCollection services,
            IWebHostEnvironment environment
            )
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc(
                    "v1",
                    new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "CSLA 8 REST API",
                        Description = string.Format("CSLA 8 model templates used in REST API ● Version {0}",
                            Assembly
                                .GetEntryAssembly()!
                                .GetCustomAttribute<AssemblyInformationalVersionAttribute>()!
                                .InformationalVersion
                        )
                    }
                );
                string xmlFile = $"{environment.ApplicationName}.xml";
                string xmlPath = Path.Combine(environment.ContentRootPath, xmlFile);
                o.IncludeXmlComments(xmlPath, true);
            });
        }

        /// <summary>
        /// Registers the Swagger middlewares.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void Use_Swagger(
            this WebApplication app
            )
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(o => o.DocExpansion(DocExpansion.None));
            }
        }
    }
}
