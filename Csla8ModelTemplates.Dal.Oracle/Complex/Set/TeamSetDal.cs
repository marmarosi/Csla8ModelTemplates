using Csla8ModelTemplates.Contracts.Complex.Set;
using Csla8RestApi.Dal;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Oracle.Complex.Set
{
    /// <summary>
    /// Implements the data access functions of the editable team collection.
    /// </summary>
    [DalImplementation]
    public class TeamSetDal : DalBase<OracleContext>, ITeamSetDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamSetDal(
            OracleContext dbContext
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
        public async Task<List<TeamSetItemDao>> FetchAsync(
            TeamSetCriteria criteria
            )
        {
            var list = await DbContext.Teams
                .Include(e => e.Players)
                .Where(e =>
                    criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName)
                )
                .Select(e => new TeamSetItemDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Players = e.Players!
                        .Select(p => new TeamSetPlayerDao
                        {
                            PlayerKey = p.PlayerKey,
                            TeamKey = p.TeamKey,
                            PlayerCode = p.PlayerCode,
                            PlayerName = p.PlayerName
                        })
                        .OrderBy(p => p.PlayerName)
                        .ToList(),
                    Timestamp = e.Timestamp
                })
                .OrderBy(o => o.TeamName)
                .AsNoTracking()
                .ToListAsync();

            return list;
        }

        #endregion GetList
    }
}
