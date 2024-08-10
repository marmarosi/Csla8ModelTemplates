using Csla8RestApi.Dal;
using Csla8RestApi.Tests.Contracts.Complex.List;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Complex.List
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
                .Include(e => e.Parts)
                .Where(e =>
                    criteria.ProductName == null || e.ProductName!.Contains(criteria.ProductName)
                )
                .Select(e => new ProductListItemDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName,
                    Parts = e.Parts!.Select(i => new ProductListPartDao
                    {
                        PartKey = i.PartKey,
                        PartCode = i.PartCode,
                        PartName = i.PartName
                    })
                    .OrderBy(io => io.PartName)
                    .ToList()
                })
                .OrderBy(o => o.ProductName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion GetList
    }
}
