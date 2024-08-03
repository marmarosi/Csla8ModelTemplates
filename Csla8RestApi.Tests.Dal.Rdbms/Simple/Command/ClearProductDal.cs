using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Simple.Command;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Simple.Command
{
    /// <summary>
    /// Implements the data access functions of the clear product command.
    /// </summary>
    [DalImplementation]
    public class ClearProductDal : DalBase<RdbmsContext>, IClearProductDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ClearProductDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Execute

        /// <summary>
        /// Executes the clear product command.
        /// </summary>
        /// <param name="dao">The data of the command.</param>
        public async Task ExecuteAsync(
            ClearProductDao dao
            )
        {
            // Get the specified product.
            var product = await DbContext.Products
                .Where(e => e.ProductKey == dao.ProductKey)
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.Product_NotFound);

            // Update the product.
            product.ProductName = dao.ProductName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new CommandFailedException(SimpleText.ClearProduct_Failed);

            // Signal successful completion.
            dao.Result = true;
        }

        #endregion
    }
}
