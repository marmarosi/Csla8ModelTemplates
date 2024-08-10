using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Csla8RestApi.Tests.Contracts.Complex.Command;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Complex.Command
{
    /// <summary>
    /// Implements the data access functions of the count products by part count command.
    /// </summary>
    [DalImplementation]
    public class CountProductsDal : DalBase<RdbmsContext>, ICountProductsDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CountProductsDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Execute

        /// <summary>
        /// Executes the count products by part count command.
        /// </summary>
        /// <param name="criteria">The criteria of the command.</param>
        public async Task<List<CountProductsResultDao>> ExecuteAsync(
            CountProductsCriteria criteria
            )
        {
            string productName = criteria.ProductName ?? "";

            var counts = await DbContext.Products
                .Include(e => e.Parts)
                .Where(e => productName == "" || e.ProductName!.Contains(productName))
                .Select(e => new { e.ProductKey, e.Parts!.Count })
                .AsNoTracking()
                .ToListAsync();

            var list = counts
                .GroupBy(
                    e => e.Count,
                    (key, grp) => new CountProductsResultDao
                    {
                        PartCount = key,
                        ProductCountByPartCount = grp.Count()
                    })
                .OrderByDescending(o => o.PartCount)
                .ToList();

            if (list.Count == 0)
                throw new CommandFailedException(ComplexText.CountProducts_CountFailed);

            return list;
        }

        #endregion
    }
}
