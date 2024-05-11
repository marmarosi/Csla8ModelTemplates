using Csla8ModelTemplates.Contracts.Simple.Set;
using Csla8RestApi.Dal;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.SqlServer.Simple.Set
{
    /// <summary>
    /// Implements the data access functions of the editable team collection.
    /// </summary>
    [DalImplementation]
    public class SimpleTeamSetDal : DalBase<SqlServerContext>, ISimpleTeamSetDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SimpleTeamSetDal(
            SqlServerContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified team set.
        /// </summary>
        /// <param name="criteria">The criteria of the team set.</param>
        /// <returns>The requested team set.</returns>
        public async Task<List<SimpleTeamSetItemDao>> FetchAsync(
            SimpleTeamSetCriteria criteria
            )
        {
            // Get the specified team set.
            var list = await DbContext.Teams
                .Where(e =>
                    criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName)
                )
                .Select(e => new SimpleTeamSetItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Timestamp = e.Timestamp
                })
                .OrderBy(o => o.TeamName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion Fetch
    }
}
