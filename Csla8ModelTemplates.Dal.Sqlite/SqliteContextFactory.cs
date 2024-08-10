using Csla8ModelTemplates.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.Sqlite
{
    /// <summary>
    /// A factory for creatings SqliteContext instances.
    /// Used by migration tool.
    /// </summary>
    public class SqliteContextFactory : IDesignTimeDbContextFactory<SqliteContext>
    {
        /// <summary>
        /// Creates a new instance of SqliteContext.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>A SqliteContext instance.</returns>
        public SqliteContext CreateDbContext(
            string[] args
            )
        {
            IConfiguration configuration = ConfigurationCreator.Create();
            var connectionString = configuration.GetValue<string>("SQLITE_CONNSTR")!;
            var assemblyName = GetType().Assembly.GetName().Name;

            return new SqliteContext(
                new DbContextOptionsBuilder<SqliteContext>()
                    .UseSqlite(
                        connectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName)
                        )
                    .Options,
                configuration
                );
        }
    }
}
