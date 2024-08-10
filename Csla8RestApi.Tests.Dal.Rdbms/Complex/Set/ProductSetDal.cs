using Csla8RestApi.Dal;
using Csla8RestApi.Tests.Contracts.Complex.Set;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Complex.Set
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
            var list = await DbContext.Products
                .Include(e => e.Parts)
                .Where(e =>
                    criteria.ProductName == null || e.ProductName!.Contains(criteria.ProductName)
                )
                .Select(e => new ProductSetItemDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName,
                    Parts = e.Parts!
                        .Select(p => new ProductSetPartDao
                        {
                            PartKey = p.PartKey,
                            ProductKey = p.ProductKey,
                            PartCode = p.PartCode,
                            PartName = p.PartName
                        })
                        .OrderBy(p => p.PartName)
                        .ToList(),
                    Timestamp = e.Timestamp
                })
                .OrderBy(o => o.ProductName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion GetList
    }
}
