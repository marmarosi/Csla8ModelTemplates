using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Dal;
using Csla8ModelTemplates.Dal.Firebird;
using Csla8RestApi.Dal;
using FirebirdSql.Data.FirebirdClient;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Csla8ModelTemplates.Configuration
{
    /// <summary>
    /// Provide methods to configure Firebird databases.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add the services to Entity Framewprk to use Firebird.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Teh application configuration.</param>
        /// <param name="detector">The dead lock detector.</param>
        public static void AddFirebirdDal(
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
            services.AddDbContext<FirebirdContext>(options =>
                options.UseFirebird(configuration.GetValue<string>("FIREBIRD_CONNSTR")!)
                );

            // Configure data access layer.
            foreach (var dalType in FirebirdDalIndex.Items)
                services.AddTransient(dalType.Key, dalType.Value);

            // Configure dead lock checking.
            if (detector is not null)
                detector.RegisterCheckMethod(
                    DAL.Firebird,
                    typeof(ConfigurationExtensions).GetMethod("IsFirebirdDeadlock")!
                    );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public static bool IsFirebirdDeadlock(
            Exception ex
            )
        {
            return ex is FbException && (ex as FbException).ErrorCode == 40001;
            //if (ex is FbException)
            //{
            //    switch ((ex as FbException).ErrorCode)
            //    {
            //        /* SQLSTATE = SQLCLASS 40 (Transaction Rollback) */
            //        case 40000: /* Ongoing transaction has been rolled back */
            //        case 40001: /* Serialization failure  */ <= THIS IS DEADLOCK
            //        case 40002: /* Transaction integrity constraint violation */
            //        case 40003: /* Statement completion unknown */
            //            return true;
            //        default:
            //            break;
            //    }
            //}
            //return false;
        }

        /// <summary>
        /// Runs Firebird seeders.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="isDevelopment">Indicates whether the hosting environment is development.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static async Task RunFirebirdSeeders(
            this IApplicationBuilder app,
            bool isDevelopment,
            string contentRootPath
            )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<FirebirdContext>();

                await FirebirdSeeder.Run(context, isDevelopment, contentRootPath);
            }
        }
    }
}
