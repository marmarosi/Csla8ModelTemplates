using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Resources;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.MySql
{
    /// <summary>
    /// A factory for creatings MySqlContext instances.
    /// Used by migration tool.
    /// </summary>
    public class MySqlContextFactory : IDesignTimeDbContextFactory<MySqlContext>
    {
        /// <summary>
        /// Creates a new instance of MySqlContext.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>A MySqlContext instance.</returns>
        public MySqlContext CreateDbContext(
            string[] args
            )
        {
            IConfiguration configuration = ConfigurationCreator.Create();
            var connectionString = configuration.GetConnectionString(DAL.MySQL)!
                .Replace("csla8modeltemplates.database", "localhost");
            var assemblyName = GetType().Assembly.GetName().Name;

            return new MySqlContext(
                new DbContextOptionsBuilder<MySqlContext>()
                    .UseMySQL(
                        connectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName)
                        )
                    .Options,
                configuration
                );
        }
    }
}
