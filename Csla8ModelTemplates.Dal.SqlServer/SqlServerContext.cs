using Csla8ModelTemplates.Entities;
using Csla8RestApi.Dal;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Csla8ModelTemplates.Dal.SqlServer
{
    /// <summary>
    /// Represents a session with the database.
    /// </summary>
    public class SqlServerContext : DbContext, ITransactionOptions
    {
        #region Constructors

        /// <summary>
        /// Indicates whether the transaction is executed in an integration test.
        /// </summary>
        public bool IsUnderTest { get; private set; }

        /// <summary>
        /// Creates a new SQL Server context instance.
        /// </summary>
        /// <param name="options">The options to be used by DbContext.</param>
        /// <param name="configuration">Teh application configuration.</param>
        public SqlServerContext(
            DbContextOptions<SqlServerContext> options,
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

        public DbSet<Team> Teams { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Folder> Folders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<GroupPerson> GroupPersons { get; set; }

        #endregion

        /// <summary>
        /// Configure the model discovered by convention from the entity type..
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        protected override void OnModelCreating(
            ModelBuilder modelBuilder
            )
        {
            #region Team

            modelBuilder.Entity<Team>()
                .HasIndex(e => e.TeamCode)
                .IsUnique();

            #endregion

            #region Player

            modelBuilder.Entity<Player>()
                .HasIndex(e => new { e.TeamKey, e.PlayerCode })
                .IsUnique();

            #endregion

            #region Folder

            modelBuilder.Entity<Folder>()
                .HasIndex(e => new { e.ParentKey, e.FolderOrder });

            #endregion

            #region Group

            modelBuilder.Entity<Group>()
                .HasIndex(e => e.GroupCode)
                .IsUnique();

            #endregion

            #region Person

            modelBuilder.Entity<Person>()
                .HasIndex(e => e.PersonCode)
                .IsUnique();

            #endregion

            #region GroupPerson

            modelBuilder.Entity<GroupPerson>()
                .HasKey(e => new { e.GroupKey, e.PersonKey });

            #endregion
        }
    }
}
