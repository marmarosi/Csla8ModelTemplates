using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Complex.Edit;
using Csla8RestApi.Tests.Entities;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Simple.Edit
{
    /// <summary>
    /// Implements the data access functions of the editable product object.
    /// </summary>
    [DalImplementation]
    public class ProductDal : DalBase<RdbmsContext>, IProductDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified product.
        /// </summary>
        /// <param name="criteria">The criteria of the product.</param>
        /// <returns>The requested product.</returns>
        public async Task<ProductDao> FetchAsync(
            ProductCriteria criteria
            )
        {
            // Get the specified product.
            var product = await DbContext.Products
                .Where(e =>
                    e.ProductKey == criteria.ProductKey
                 )
                .Select(e => new ProductDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName,
                    Timestamp = e.Timestamp
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.Product_NotFound);

            return product;
        }

        #endregion Fetch

        #region Insert

        /// <summary>
        /// Creates a new product using the specified data.
        /// </summary>
        /// <param name="dao">The data of the product.</param>
        public async Task InsertAsync(
            ProductDao dao
            )
        {
            // Check unique product code.
            var product = await DbContext.Products
                .Where(e =>
                    e.ProductCode == dao.ProductCode
                )
                .FirstOrDefaultAsync();
            if (product is not null)
                throw new DataExistException(SimpleText.Product_ProductCodeExists.With(dao.ProductCode!));

            // Create the new product.
            product = new Product
            {
                ProductCode = dao.ProductCode,
                ProductName = dao.ProductName
            };
            await DbContext.Products.AddAsync(product);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(SimpleText.Product_InsertFailed);

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
            ProductDao dao
            )
        {
            // Get the specified product.
            var product = await DbContext.Products
                .Where(e =>
                    e.ProductKey == dao.ProductKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.Product_NotFound);
            if (product.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(SimpleText.Product_Concurrency);

            // Check unique product code.
            if (product.ProductCode != dao.ProductCode)
            {
                int exist = await DbContext.Products
                    .Where(e => e.ProductCode == dao.ProductCode && e.ProductKey != product.ProductKey)
                    .CountAsync();
                if (exist > 0)
                    throw new DataExistException(SimpleText.Product_ProductCodeExists.With(dao.ProductCode!));
            }

            // Update the product.
            product.ProductCode = dao.ProductCode;
            product.ProductName = dao.ProductName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(SimpleText.Product_UpdateFailed);

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
            ProductCriteria criteria
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
                ?? throw new DataNotFoundException(SimpleText.Product_NotFound);

            // Check references.
            //int dependents = 0;

            //dependents = await DbContext.Others.CountAsync(e => e.ProductKey == criteria.ProductKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(SimpleText.Product_Delete_Others);

            // Delete references.
            var parts = await DbContext.Parts
                .Where(e => e.ProductKey == criteria.ProductKey)
                .ToListAsync();
            foreach (var part in parts)
                DbContext.Parts.Remove(part);

            count = await DbContext.SaveChangesAsync();
            if (count != parts.Count)
                throw new DeleteFailedException(SimpleText.Product_Delete_Parts);

            // Delete the product.
            DbContext.Products.Remove(product);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(SimpleText.Product_DeleteFailed);
        }

        #endregion Delete
    }
}
