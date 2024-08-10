using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Tests.Contracts.Selection.ByKey;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Selection.ByKey
{
    /// <summary>
    /// Implements the data access functions of the read-only product choice collection.
    /// </summary>
    [DalImplementation]
    public class ProductChoiceDal : DalBase<RdbmsContext>, IProductChoiceDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public ProductChoiceDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the choice of the products.
        /// </summary>
        /// <param name="criteria">The criteria of the product choice.</param>
        /// <returns>The data transfer object of the requested product choice.</returns>
        public async Task<List<ChoiceItemDao<long?>>> FetchAsync(
            ProductChoiceCriteria criteria
            )
        {
            var choice = await DbContext.Products
                .Where(e => criteria.ProductName == null || e.ProductName!.Contains(criteria.ProductName))
                .Select(e => new ChoiceItemDao<long?>
                {
                    Value = e.ProductKey,
                    Name = e.ProductName
                })
                .OrderBy(o => o.Name)
                .AsNoTracking()
                .ToListAsync();

            return choice;
        }

        #endregion GetChoice
    }
}
