namespace Csla8ModelTemplates.WebApi.Extensions
{
    /// <summary>
    /// Provides methods to configure CORS.
    /// </summary>
    internal static class CorsExtensions
    {
        /// <summary>
        /// Configure CORS to limit the API availability.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="environment">The hosting environment.</param>
        public static void Add_Cors(
            this IServiceCollection services,
            IWebHostEnvironment environment
            )
        {
            var configuration = services.BuildServiceProvider().GetService<IConfiguration>();

            if (environment.IsProduction())
            {
                var allowedHosts = configuration!.GetValue<string>("AllowedHosts")!.Split(';');
                services.AddCors(options => options.AddPolicy(
                    "Csla8ModelTemplatesPolicy",
                    policyBuilder => policyBuilder
                        .WithOrigins(allowedHosts)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        )
                );
            }
        }

        /// <summary>
        /// Use CORS in the application.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void Use_Cors(
            this WebApplication app
            )
        {
            if (app.Environment.IsProduction())
            {
                app.UseCors();
            }
        }
    }
}
