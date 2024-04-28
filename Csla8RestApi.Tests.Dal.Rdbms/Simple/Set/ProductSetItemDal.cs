using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Simple.Set;
using Csla8RestApi.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Simple.Set
{
    /// <summary>
    /// Implements the data access functions of the editable product set item object.
    /// </summary>
    [DalImplementation]
    public class ProductSetItemDal : DalBase<RdbmsContext>, IProductSetItemDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductSetItemDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Insert

        /// <summary>
        /// Creates a new product using the specified data.
        /// </summary>
        /// <param name="dao">The data of the product.</param>
        public async Task InsertAsync(
            ProductSetItemDao dao
            )
        {
            // Check unique product code.
            var product = await DbContext.Products
                .Where(e =>
                    e.ProductCode == dao.ProductCode
                )
                .FirstOrDefaultAsync();
            if (product is not null)
                throw new DataExistException(SimpleText.ProductSetItem_ProductCodeExists.With(dao.ProductCode!));

            // Create the new product.
            product = new Product
            {
                ProductCode = dao.ProductCode,
                ProductName = dao.ProductName
            };
            await DbContext.Products.AddAsync(product);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(SimpleText.ProductSetItem_InsertFailed.With(product.ProductCode!));

            // Return new data.
            dao.ProductKey = product.ProductKey;
            dao.Timestamp = product.Timestamp;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing product using the specified data.
        /// </summary>
        /// <param name="dao">The data of the product.</param>
        public async Task UpdateAsync(
            ProductSetItemDao dao
            )
        {
            // Get the specified product.
            var product = await DbContext.Products
                .Where(e =>
                    e.ProductKey == dao.ProductKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.ProductSetItem_NotFound.With(dao.ProductCode!));
            if (product.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(SimpleText.ProductSetItem_Concurrency.With(dao.ProductCode!));

            // Check unique product code.
            if (product.ProductCode != dao.ProductCode)
            {
                int exist = await DbContext.Products
                    .Where(e =>
                        e.ProductCode == dao.ProductCode &&
                        e.ProductKey != product.ProductKey
                    )
                    .CountAsync();
                if (exist > 0)
                    throw new DataExistException(SimpleText.ProductSetItem_ProductCodeExists.With(dao.ProductCode!));
            }

            // Update the product.
            product.ProductCode = dao.ProductCode;
            product.ProductName = dao.ProductName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(SimpleText.ProductSetItem_UpdateFailed.With(product.ProductCode!));

            // Return new data.
            dao.Timestamp = product.Timestamp;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="criteria">The criteria of the product.</param>
        public async Task DeleteAsync(
            ProductSetItemCriteria criteria
            )
        {
            int count = 0;

            // Get the specified product.
            var product = await DbContext.Products
                .Where(e =>
                    e.ProductKey == criteria.ProductKey
                 )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.ProductSetItem_NotFoundKey);

            // Check references.
            //int dependents = 0;

            //dependents = await DbContext.Others.CountAsync(e => e.ProductKey == criteria.ProductKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(SimpleText.ProductSetItem_Delete_Others);

            // Delete references.
            var parts = await DbContext.Parts
                .Where(e => e.ProductKey == criteria.ProductKey)
                .ToListAsync();
            foreach (var part in parts)
                DbContext.Parts.Remove(part);

            count = await DbContext.SaveChangesAsync();
            if (count != parts.Count)
                throw new DeleteFailedException(SimpleText.ProductSetItem_Delete_Parts.With(product.ProductCode!));

            // Delete the product.
            DbContext.Products.Remove(product);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(SimpleText.ProductSetItem_DeleteFailed.With(product.ProductCode!));
        }

        #endregion Delete
    }
}