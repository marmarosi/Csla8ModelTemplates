using Csla8ModelTemplates.Contracts.Complex.List;
using Csla8RestApi.Dal;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.MySql.Complex.List
{
    /// <summary>
    /// Implements the data access functions of the read-only team collection.
    /// </summary>
    [DalImplementation]
    public class TeamListDal : DalBase<MySqlContext>, ITeamListDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamListDal(
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
        /// <returns>The requested team items.</returns>
        public async Task<List<TeamListItemDao>> FetchAsync(
            TeamListCriteria criteria
            )
        {
            var list = await DbContext.Teams
                .Include(e => e.Players)
                .Where(e =>
                    criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName)
                )
                .Select(e => new TeamListItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Players = e.Players!.Select(i => new TeamListPlayerDao
                    {
                        PlayerKey = i.PlayerKey,
                        PlayerCode = i.PlayerCode,
                        PlayerName = i.PlayerName
                    })
                    .OrderBy(io => io.PlayerName)
                    .ToList()
                })
                .OrderBy(o => o.TeamName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion GetList
    }
}
