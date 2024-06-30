using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Dal;
using Csla8ModelTemplates.Dal.Db2;
using Csla8RestApi.Dal;
using IBM.Data.Db2;
using IBM.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csla8ModelTemplates.Configuration
{
    /// <summary>
    /// Provide methods to configure DB2 databases.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add the services to Entity Framewprk to use DB2.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Teh application configuration.</param>
        /// <param name="detector">The dead lock detector.</param>
        public static void AddDb2Dal(
            this IServiceCollection services,
            IConfiguration? configuration = null,
            IDeadLockDetector? detector = null
            )
        {
            // Configure database.
            if (configuration is null)
            {
                configuration = ConfigurationCreator.Create();
            }
            services.AddDbContext<Db2Context>(options =>
                options.UseDb2(configuration.GetValue<string>("DB2_CONNSTR")!, null)
                );

            // Configure data access layer.
            foreach (var dalType in Db2DalIndex.Items)
                services.AddTransient(dalType.Key, dalType.Value);

            // Configure dead lock checking.
            if (detector is not null)
                detector.RegisterCheckMethod(
                    DAL.DB2,
                    typeof(ConfigurationExtensions).GetMethod("IsDb2Deadlock")!
                    );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public static bool IsDb2Deadlock(
            Exception ex
            )
        {
            return ex is DB2Exception && (ex as DB2Exception)!.ErrorCode == 1213;
        }

        /// <summary>
        /// Runs DB2 seeders.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="isDevelopment">Indicates whether the hosting environment is development.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static async Task RunDb2Seeders(
            this IApplicationBuilder app,
            bool isDevelopment,
            string contentRootPath
            )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<Db2Context>();

                await Db2Seeder.Run(context, isDevelopment, contentRootPath);
            }
        }
    }
}
