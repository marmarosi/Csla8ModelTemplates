using Csla8ModelTemplates.Contracts.Arrangement.Pagination;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Firebird.Arrangement.Pagination
{
    /// <summary>
    /// Implements the data access functions of the read-only paginated team collection.
    /// </summary>
    [DalImplementation]
    public class PaginatedTeamListDal : DalBase<FirebirdContext>, IPaginatedTeamListDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public PaginatedTeamListDal(
            FirebirdContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified page of teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team list.</param>
        /// <returns>The requested page of the team list.</returns>
        public async Task<IPaginatedList<PaginatedTeamListItemDao>> FetchAsync(
            PaginatedTeamListCriteria criteria
            )
        {
            // Filter the teams.
            var query = DbContext.Teams
                .Where(e =>
                    criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName)
                );

            // Get the requested page.
            var list = await query
                .Select(e => new PaginatedTeamListItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName
                })
                .OrderBy(o => o.TeamName)
                .Skip(criteria.PageIndex * criteria.PageSize)
                .Take(criteria.PageSize)
                .AsNoTracking()
                .ToListAsync();

            // Count the matching teams.
            int totalCount = await query.CountAsync();

            // Return the result.
            return new PaginatedList<PaginatedTeamListItemDao>
            {
                Data = list,
                TotalCount = totalCount
            };
        }

        #endregion GetList
    }
}
