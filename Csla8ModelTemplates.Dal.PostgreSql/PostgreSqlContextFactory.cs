using Csla8ModelTemplates.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.PostgreSql
{
    /// <summary>
    /// A factory for creatings PostgreSqlContext instances.
    /// Used by migration tool.
    /// </summary>
    public class PostgreSqlContextFactory : IDesignTimeDbContextFactory<PostgreSqlContext>
    {
        /// <summary>
        /// Creates a new instance of PostgreSqlContext.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>A PostgreSqlContext instance.</returns>
        public PostgreSqlContext CreateDbContext(
            string[] args
            )
        {
            IConfiguration configuration = ConfigurationCreator.Create();
            var connectionString = configuration.GetValue<string>("POSTGRESQL_CONNSTR")!;
            var assemblyName = GetType().Assembly.GetName().Name;

            return new PostgreSqlContext(
                new DbContextOptionsBuilder<PostgreSqlContext>()
                    .UseNpgsql(
                        connectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName)
                        )
                    .Options,
                configuration
                );
        }
    }
}
