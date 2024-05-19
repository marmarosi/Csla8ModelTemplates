using Csla8ModelTemplates.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.Oracle
{
    /// <summary>
    /// A factory for creatings OracleContext instances.
    /// Used by migration tool.
    /// </summary>
    public class OracleContextFactory : IDesignTimeDbContextFactory<OracleContext>
    {
        /// <summary>
        /// Creates a new instance of OracleContext.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>A OracleContext instance.</returns>
        public OracleContext CreateDbContext(
            string[] args
            )
        {
            IConfiguration configuration = ConfigurationCreator.Create();
            var connectionString = configuration.GetConnectionString(DAL.MySQL)!
                .Replace("csla8mt.database", "localhost");
            var assemblyName = GetType().Assembly.GetName().Name;

            return new OracleContext(
                new DbContextOptionsBuilder<OracleContext>()
                    .UseOracle(
                        connectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName)
                        )
                    .Options,
                configuration
                );
        }
    }
}
