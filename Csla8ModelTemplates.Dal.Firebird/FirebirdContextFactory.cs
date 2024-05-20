using Csla8ModelTemplates.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.Firebird
{
    /// <summary>
    /// A factory for creatings FirebirdContext instances.
    /// Used by migration tool.
    /// </summary>
    public class FirebirdContextFactory : IDesignTimeDbContextFactory<FirebirdContext>
    {
        /// <summary>
        /// Creates a new instance of FirebirdContext.
        /// </summary>
        /// <param name="args">Arguments provided by the design-time service.</param>
        /// <returns>A FirebirdContext instance.</returns>
        public FirebirdContext CreateDbContext(
            string[] args
            )
        {
            IConfiguration configuration = ConfigurationCreator.Create();
            var connectionString = configuration.GetConnectionString(DAL.Firebird)!
                .Replace("csla8mt.database", "localhost");
            var assemblyName = GetType().Assembly.GetName().Name;

            return new FirebirdContext(
                new DbContextOptionsBuilder<FirebirdContext>()
                    .UseFirebird(
                        connectionString,
                        optionsBuilder => optionsBuilder.MigrationsAssembly(assemblyName)
                        )
                    .Options,
                configuration
                );
        }
    }
}
