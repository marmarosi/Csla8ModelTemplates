using Csla8ModelTemplates.Contracts.Simple.List;
using Csla8RestApi.Dal;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Sqlite.Simple.List
{
    /// <summary>
    /// Implements the data access functions of the read-only team collection.
    /// </summary>
    [DalImplementation]
    public class SimpleTeamListDal : DalBase<SqliteContext>, ISimpleTeamListDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SimpleTeamListDal(
            SqliteContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested team items.</returns>
        public async Task<List<SimpleTeamListItemDao>> FetchAsync(
            SimpleTeamListCriteria criteria
            )
        {
            var list = await DbContext.Teams
                .Where(e =>
                    criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName)
                )
                .Select(e => new SimpleTeamListItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName
                })
                .OrderBy(o => o.TeamName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion Fetch
    }
}
