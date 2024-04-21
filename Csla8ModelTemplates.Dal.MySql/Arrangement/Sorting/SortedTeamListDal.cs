using Csla8ModelTemplates.Contracts.Arrangement.Sorting;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.MySql.Arrangement.Sorting
{
    /// <summary>
    /// Implements the data access functions of the read-only sorted team collection.
    /// </summary>
    [DalImplementation]
    public class SortedTeamListDal : DalBase<MySqlContext>, ISortedTeamListDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SortedTeamListDal(
            MySqlContext dbContext
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
        /// <returns>The requested team list.</returns>
        public async Task<List<SortedTeamListItemDao>> FetchAsync(
            SortedTeamListCriteria criteria
            )
        {
            // Filter the teams.
            var query = DbContext.Teams
                .Where(e =>
                    criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName)
                )
                .Select(e => new SortedTeamListItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName
                });

            // Sort the items.
            switch (criteria.SortBy)
            {
                case SortedTeamListSortBy.TeamCode:
                    query = criteria.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(e => e.TeamCode)
                        : query.OrderByDescending(e => e.TeamCode);
                    break;
                //case SortedTeamListSortBy.TeamName:
                default:
                    query = criteria.SortDirection == SortDirection.Ascending
                        ? query.OrderBy(e => e.TeamName)
                        : query.OrderByDescending(e => e.TeamName);
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
