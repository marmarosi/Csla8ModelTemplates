using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Complex.View;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Complex.View
{
    /// <summary>
    /// Implements the data access functions of the read-only product object.
    /// </summary>
    [DalImplementation]
    public class ProductViewDal : DalBase<RdbmsContext>, IProductViewDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductViewDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified product view.
        /// </summary>
        /// <param name="criteria">The criteria of the product.</param>
        /// <returns>The requested product view.</returns>
        public async Task<ProductViewDao> FetchAsync(
            ProductViewCriteria criteria
            )
        {
            // Get the specified product.
            var product = await DbContext.Products
                .Include(e => e.Parts)
                .Where(e =>
                    e.ProductKey == criteria.ProductKey
                 )
                .Select(e => new ProductViewDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName,
                    Parts = e.Parts!
                        .Select(p => new ProductViewPartDao
                        {
                            PartKey = p.PartKey,
                            PartCode = p.PartCode,
                            PartName = p.PartName
                        })
                    .OrderBy(p => p.PartName)
                    .ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.Product_NotFound);

            return product;
        }

        #endregion Fetch
    }
}
