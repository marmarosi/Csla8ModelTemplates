using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Dal.MySql;
using Csla8ModelTemplates.Resources;
using Csla8RestApi.Dal;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MySql.Data.MySqlClient;

namespace Csla8ModelTemplates.Configuration
{
    /// <summary>
    /// Provide methods to configure MySQL databases.
    /// </summary>
    public static class ConfigurationExtensions
    {
        /// <summary>
        /// Add the services to Entity Framewprk to use MySQL.
        /// </summary>
        /// <param name="services">The service collection.</param>
        /// <param name="configuration">Teh application configuration.</param>
        /// <param name="detector">The dead lock detector.</param>
        public static void AddMySqlDal(
            this IServiceCollection services,
            IConfiguration? configuration = null,
            IDeadLockDetector? detector = null
            )
        {
            // Configure database.
            if (configuration is null)
            {
                //services.AddDbContext<MySqlContext>(options => options
                //    .UseMySQL($"name=ConnectionStrings:{DAL.MySQL}")
                //    );
                configuration = ConfigurationCreator.Create();
                services.AddDbContext<MySqlContext>(options => options
                    .UseMySQL(configuration.GetConnectionString(DAL.MySQL)!)
                    );
            }
            else
                services.AddDbContext<MySqlContext>(options =>
                    options.UseMySQL(configuration.GetConnectionString(DAL.MySQL)!)
                    );

            // Configure data access layer.
            foreach (var dalType in MySqlDalIndex.Items)
                services.AddTransient(dalType.Key, dalType.Value);

            // Configure dead lock checking.
            if (detector is not null)
                detector.RegisterCheckMethod(
                    DAL.MySQL,
                    typeof(ConfigurationExtensions).GetMethod("IsMySqlDeadlock")!
                    );
        }

        /// <summary>
        /// CHecks whether the reason of the exception is a deadlock.
        /// </summary>
        /// <param name="ex">The original exception thrown.</param>
        /// <returns>True when the reason is a deadlock; otherwise false;</returns>
        public static bool IsMySqlDeadlock(
            Exception ex
            )
        {
            return ex is MySqlException && (ex as MySqlException)!.Number == 1213;
        }

        /// <summary>
        /// Runs MySQL seeders.
        /// </summary>
        /// <param name="app">The application builder.</param>
        /// <param name="isDevelopment">Indicates whether the hosting environment is development.</param>
        /// <param name="contentRootPath">The root path of the web site.</param>
        public static void RunMySqlSeeders(
            this IApplicationBuilder app,
            bool isDevelopment,
            string contentRootPath
            )
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<MySqlContext>();

                MySqlSeeder.Run(context, isDevelopment, contentRootPath);
            }
        }
    }
}
