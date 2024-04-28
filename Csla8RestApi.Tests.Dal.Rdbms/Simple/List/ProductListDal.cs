using Csla8RestApi.Dal;
using Csla8RestApi.Tests.Contracts.Simple.List;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Simple.List
{
    /// <summary>
    /// Implements the data access functions of the read-only product collection.
    /// </summary>
    [DalImplementation]
    public class ProductListDal : DalBase<RdbmsContext>, IProductListDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductListDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified products.
        /// </summary>
        /// <param name="criteria">The criteria of the product list.</param>
        /// <returns>The requested product items.</returns>
        public async Task<List<ProductListItemDao>> FetchAsync(
            ProductListCriteria criteria
            )
        {
            var list = await DbContext.Products
                .Where(e =>
                    criteria.ProductName == null || e.ProductName!.Contains(criteria.ProductName)
                )
                .Select(e => new ProductListItemDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName
                })
                .OrderBy(o => o.ProductName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion Fetch
    }
}