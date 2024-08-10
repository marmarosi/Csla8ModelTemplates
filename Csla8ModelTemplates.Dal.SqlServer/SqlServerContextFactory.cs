using Csla8ModelTemplates.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.SqlServer
{
    /// <summary>
    /// A factory for creatings SqlServerContext instances.
    /// Used by migration tool.
    /// </summary>
    public class SqlServerContextFactory : IDesignTimeDbContextFactory<SqlServerContext>
    {
        /// <summary>
        /// Creates a new instance of SqlServerContext.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>A SqlServerContext instance.</returns>
        public SqlServerContext CreateDbContext(
            string[] args
            )
        {
            IConfiguration configuration = ConfigurationCreator.Create();
            var connectionString = configuration.GetValue<string>("SQLSERVER_CONNSTR")!;
            var assemblyName = GetType().Assembly.GetName().Name;

            return new SqlServerContext(
                new DbContextOptionsBuilder<SqlServerContext>()
                    .UseSqlServer(
                        connectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName)
                        )
                    .Options,
                configuration
                );
        }
    }
}
