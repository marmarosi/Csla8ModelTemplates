using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Simple.View;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.MySql.Simple.View
{
    /// <summary>
    /// Implements the data access functions of the read-only product model.
    /// </summary>
    [DalImplementation]
    public partial class ProductViewDal : DalBase<MySqlContext>, IProductViewDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductViewDal(
            MySqlContext dbContext
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
                .Where(e =>
                    e.ProductKey == criteria.ProductKey
                 )
                .Select(e => new ProductViewDao
                {
                    ProductKey = e.ProductKey,
                    ProductCode = e.ProductCode,
                    ProductName = e.ProductName
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.Product_NotFound);

            return product;
        }

        #endregion Fetch
    }
}
