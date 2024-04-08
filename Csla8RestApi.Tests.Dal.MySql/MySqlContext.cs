using Csla8RestApi.Dal;
using Csla8RestApi.Tests.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Csla8RestApi.Tests.Dal.MySql
{
    /// <summary>
    /// Represents a session with the database.
    /// </summary>
    public class MySqlContext : DbContext, ITransactionOptions
    {
        #region Constructors

        /// <summary>
        /// Indicates whether the transaction is executed in an integration test.
        /// </summary>
        public bool IsUnderTest { get; private set; }

        /// <summary>
        /// Creates a new MySQL context instance.
        /// </summary>
        /// <param name="options">The options to be used by DbContext.</param>
        /// <param name="configuration">Teh application configuration.</param>
        public MySqlContext(
            DbContextOptions<MySqlContext> options,
            IConfiguration configuration
            )
            : base(options)
        {
            IsUnderTest = configuration.GetValue<bool>("RollbackTransactions");
        }

        #endregion

        #region Auto update timestamps

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges()
        {
            SetTimestamps();
            return base.SaveChanges();
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether AcceptAllChanges() is called
        ///     after the changes have been sent successfully ti the database.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public override int SaveChanges(
            bool acceptAllChangesOnSuccess
            )
        {
            SetTimestamps();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="cancellationToken">A CancellationToken to observe
        ///     while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public override Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default
            )
        {
            SetTimestamps();
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        /// <param name="acceptAllChangesOnSuccess">Indicates whether AcceptAllChanges() is called
        ///     after the changes have been sent successfully ti the database.</param>
        /// <param name="cancellationToken">A CancellationToken to observe
        ///     while waiting for the task to complete.</param>
        /// <returns>The number of state entries written to the database.</returns>
        public override Task<int> SaveChangesAsync(
            bool acceptAllChangesOnSuccess,
            CancellationToken cancellationToken = default
            )
        {
            SetTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void SetTimestamps()
        {
            var insertedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Added)
                .Select(x => x.Entity);

            //System.Diagnostics.Debug.WriteLine($"Inserted: {insertedEntries.Count()}");
            foreach (var insertedEntry in insertedEntries)
            {
                var timestamped = insertedEntry as Timestamped;
                // If the inserted object has timestamp.
                if (timestamped is not null)
                {
                    timestamped.Timestamp = DateTimeOffset.UtcNow;
                }
            }

            var modifiedEntries = ChangeTracker.Entries()
                .Where(x => x.State == EntityState.Modified)
                .Select(x => x.Entity);

            //System.Diagnostics.Debug.WriteLine($"Inserted: {modifiedEntries.Count()}");
            foreach (var modifiedEntry in modifiedEntries)
            {
                var timestamped = modifiedEntry as Timestamped;
                // If the modified object has timestamp.
                if (timestamped is not null)
                {
                    timestamped.Timestamp = DateTimeOffset.UtcNow;
                }
            }
        }

        #endregion

        #region Query results

        public DbSet<Product> Products { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        #endregion

        /// <summary>
        /// Configure the model discovered by convention from the entity type..
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(
            ModelBuilder modelBuilder
            )
        {
            #region Order

            modelBuilder.Entity<Product>()
                .HasIndex(e => e.ProductCode)
                .IsUnique();

            #endregion

            #region OrderLine

            modelBuilder.Entity<Part>()
                .HasIndex(e => new { e.PeoductKey, e.PartKey })
                .IsUnique();

            #endregion

            #region Message

            modelBuilder.Entity<Message>()
                .HasIndex(e => new { e.ParentKey, e.MessageOrder });

            #endregion

            #region Role

            modelBuilder.Entity<Role>()
                .HasIndex(e => e.RoleCode)
                .IsUnique();

            #endregion

            #region User

            modelBuilder.Entity<User>()
                .HasIndex(e => e.UserCode)
                .IsUnique();

            #endregion

            #region UserRole

            modelBuilder.Entity<UserRole>()
                .HasKey(e => new { e.RoleKey, e.UserKey });

            #endregion
        }
    }
}
