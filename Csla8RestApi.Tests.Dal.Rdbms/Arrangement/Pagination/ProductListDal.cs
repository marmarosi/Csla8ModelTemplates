using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Tests.Contracts.Arrangement.Pagination;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Arrangement.Pagination
{
    /// <summary>
    /// Implements the data access functions of the read-only paginated product collection.
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
        /// Gets the specified page of products.
        /// </summary>
        /// <param name="criteria">The criteria of the product list.</param>
        /// <returns>The requested page of the product list.</returns>
        public async Task<IPaginatedList<ProductListItemDao>> FetchAsync(
            ProductListCriteria criteria
            )
        {
            // Filter the products.
            var query = DbContext.Products
                .Where(e =>
                    criteria.ProductName == null || e.ProductName!.Contains(criteria.ProductName)
                );

            // Get the requested page.
            var list = await query
                .Select(e => new ProductListItemDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName
                })
                .OrderBy(o => o.ProductName)
                .Skip(criteria.PageIndex * criteria.PageSize)
                .Take(criteria.PageSize)
                .AsNoTracking()
                .ToListAsync();

            // Count the matching products.
            int totalCount = await query.CountAsync();

            // Return the result.
            return new PaginatedList<ProductListItemDao>
            {
                Data = list,
                TotalCount = totalCount
            };
        }

        #endregion GetList
    }
}
