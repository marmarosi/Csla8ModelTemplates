using Csla8ModelTemplates.Configuration;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Dal;
using Csla8ModelTemplates.Resources;

namespace Csla8ModelTemplates.WebApi.Extensions
{
    /// <summary>
    /// Provide methods to configure data access layers.
    /// </summary>
    internal static class DataAccessExtensions
    {
        private static IConfiguration? _configuration;

        /// <summary>
        /// Add the services to Entity Framewprk to use data access layers.
        /// </summary>
        /// <param name="services">The service collection.</param>
        public static void Add_DataAccessLayers(
            this IServiceCollection services
            )
        {
            _configuration = services.BuildServiceProvider().GetService<IConfiguration>();
            var dalNames = _configuration!.GetSection("ActiveDals").Get<List<string>>();

            IDeadLockDetector detector = new DeadLockDetector();
            services.AddSingleton(detector);

            foreach (var dalName in dalNames!)
            {
                switch (dalName)
                {
                    //case DAL.DB2:
                    //    services.AddDb2Dal(detector);
                    //    break;
                    //case DAL.Firebird:
                    //    services.AddFirebirdDal(detector);
                    //    break;
                    case DAL.MySQL:
                        services.AddMySqlDal(detector);
                        break;
                    //case DAL.Oracle:
                    //    services.AddOracleDal(detector);
                    //    break;
                    //case DAL.PostgreSQL:
                    //    services.AddPostgreSqlDal(detector);
                    //    break;
                    //case DAL.SQLite:
                    //    services.AddSqliteDal(detector);
                    //    break;
                    //case DAL.SQLServer:
                    //    services.AddSqlServerDal(detector);
                    //    break;
                }
            }
            services.AddSingleton(typeof(ITransactionOptions), new TransactionOptions(false));
        }

        /// <summary>
        /// Runs seeders of persistent storages.
        /// </summary>
        /// <param name="app">The web application.</param>
        public static void Run_StorageSeeders(
            this WebApplication app
            )
        {
            var dalNames = _configuration!.GetSection("ActiveDals").Get<List<string>>();
            var isDevelopment = app.Environment.IsDevelopment();
            var contentRootPath = app.Environment.ContentRootPath;

            foreach (var dalName in dalNames!)
            {
                switch (dalName)
                {
                    //case DAL.DB2:
                    //    app.RunDb2Seeders(isDevelopment, contentRootPath);
                    //    break;
                    //case DAL.Firebird:
                    //    app.RunFirebirdSeeders(isDevelopment, contentRootPath);
                    //    break;
                    case DAL.MySQL:
                        app.RunMySqlSeeders(isDevelopment, contentRootPath);
                        break;
                    //case DAL.Oracle:
                    //    app.RunOracleSeeders(isDevelopment, contentRootPath);
                    //    break;
                    //case DAL.PostgreSQL:
                    //    app.RunPostgreSqlSeeders(isDevelopment, contentRootPath);
                    //    break;
                    //case DAL.SQLite:
                    //    app.RunSqliteSeeders(isDevelopment, contentRootPath);
                    //    break;
                    //case DAL.SQLServer:
                    //    app.RunSqlServerSeeders(isDevelopment, contentRootPath);
                    //    break;
                }
            }
        }
    }
}
