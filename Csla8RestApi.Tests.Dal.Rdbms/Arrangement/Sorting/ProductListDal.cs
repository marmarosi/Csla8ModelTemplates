using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Tests.Contracts.Arrangement.Sorting;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Arrangement.Sorting
{
    /// <summary>
    /// Implements the data access functions of the read-only sorted product collection.
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
        /// <returns>The requested product list.</returns>
        public async Task<List<ProductListItemDao>> FetchAsync(
            ProductListCriteria criteria
            )
        {
            // Filter the products.
            var query = DbContext.Products
                .Where(e =>
                    criteria.ProductName == null || e.ProductName!.Contains(criteria.ProductName)
                )
                .Select(e => new ProductListItemDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName
                });

            // Sort the items.
            switch (criteria.SortBy)
            {
                case ProductListSortBy.ProductCode:
                    query = criteria.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(e => e.ProductCode)
                        : query.OrderByDescending(e => e.ProductCode);
                    break;
                //case ProductListSortBy.ProductName:
                default:
                    query = criteria.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(e => e.ProductName)
                        : query.OrderByDescending(e => e.ProductName);
                    break;
            }

            // Return the result.
            var list = await query
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion GetList
    }
}
