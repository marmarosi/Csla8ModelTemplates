using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Csla8RestApi.Dal
{
    /// <summary>
    /// Implements the functionality of a data access layer implementation.
    /// </summary>
    public class DalBase<T> : ITransactionalDal where T : DbContext, ITransactionOptions
    {
        /// <summary>
        /// The database context.
        /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public T DbContext { get; protected set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Begins a new database transaction.
        /// </summary>
        /// <returns>The database transaction.</returns>
        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await DbContext.Database.BeginTransactionAsync();
        }

        /// <summary>
        /// Commits the specified transaction when it is not executed in integration test.
        /// </summary>
        /// <param name="transaction">The current database transaction to commit.</param>
        public async Task Commit(
            IDbContextTransaction transaction
            )
        {
            if (!DbContext.IsUnderTest)
                await transaction.CommitAsync();
        }
    }
}
