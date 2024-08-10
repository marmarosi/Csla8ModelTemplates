using Csla8RestApi.Dal;
using Csla8RestApi.Tests.Contracts.Simple.Set;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Simple.Set
{
    /// <summary>
    /// Implements the data access functions of the editable product collection.
    /// </summary>
    [DalImplementation]
    public class ProductSetDal : DalBase<RdbmsContext>, IProductSetDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductSetDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified product set.
        /// </summary>
        /// <param name="criteria">The criteria of the product set.</param>
        /// <returns>The requested product set.</returns>
        public async Task<List<ProductSetItemDao>> FetchAsync(
            ProductSetCriteria criteria
            )
        {
            // Get the specified product set.
            var list = await DbContext.Products
                .Where(e =>
                    criteria.ProductName == null || e.ProductName!.Contains(criteria.ProductName)
                )
                .Select(e => new ProductSetItemDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName,
                    Timestamp = e.Timestamp
                })
                .OrderBy(o => o.ProductName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion Fetch
    }
}
