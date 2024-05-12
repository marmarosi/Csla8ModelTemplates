using Csla8ModelTemplates.Contracts.Complex.View;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Oracle.Complex.View
{
    /// <summary>
    /// Implements the data access functions of the read-only team object.
    /// </summary>
    [DalImplementation]
    public class TeamViewDal : DalBase<OracleContext>, ITeamViewDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamViewDal(
            OracleContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the specified team view.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        /// <returns>The requested team view.</returns>
        public async Task<TeamViewDao> FetchAsync(
            TeamViewCriteria criteria
            )
        {
            // Get the specified team.
            var team = await DbContext.Teams
                .Include(e => e.Players)
                .Where(e =>
                    e.TeamKey == criteria.TeamKey
                 )
                .Select(e => new TeamViewDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName,
                    Players = e.Players!
                        .Select(p => new TeamViewPlayerDao
                        {
                            PlayerKey = p.PlayerKey,
                            PlayerCode = p.PlayerCode,
                            PlayerName = p.PlayerName
                        })
                    .OrderBy(p => p.PlayerName)
                    .ToList()
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.Team_NotFound);

            return team;
        }

        #endregion Fetch
    }
}
