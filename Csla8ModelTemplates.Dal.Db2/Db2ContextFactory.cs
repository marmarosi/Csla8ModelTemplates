using Csla8ModelTemplates.Contracts;
using IBM.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.Db2
{
    /// <summary>
    /// A factory for creatings Db2Context instances.
    /// Used by migration tool.
    /// </summary>
    public class Db2ContextFactory : IDesignTimeDbContextFactory<Db2Context>
    {
        /// <summary>
        /// Creates a new instance of Db2Context.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>A Db2Context instance.</returns>
        public Db2Context CreateDbContext(
            string[] args
            )
        {
            IConfiguration configuration = ConfigurationCreator.Create();
            var connectionString = configuration.GetConnectionString(DAL.DB2)!
                .Replace("csla8mt.database", "localhost");
            var assemblyName = GetType().Assembly.GetName().Name;

            return new Db2Context(
                new DbContextOptionsBuilder<Db2Context>()
                    .UseDb2(
                        connectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName)
                        )
                    .Options,
                configuration
                );
        }
    }
}
