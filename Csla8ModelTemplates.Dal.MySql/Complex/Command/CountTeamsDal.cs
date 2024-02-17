using Csla8ModelTemplates.Contracts.Complex.Command;
using Csla8ModelTemplates.Resources;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.MySql.Complex.Command
{
    /// <summary>
    /// Implements the data access functions of the count teams by player count command.
    /// </summary>
    [DalImplementation]
    public class CountTeamsDal : DalBase<MySqlContext>, ICountTeamsDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CountTeamsDal(
            MySqlContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Execute

        /// <summary>
        /// Counts the teams grouped by the number of their players.
        /// </summary>
        /// <param name="criteria">The criteria of the command.</param>
        public List<CountTeamsResultDao> Execute(
            CountTeamsCriteria criteria
            )
        {
            string teamName = criteria.TeamName ?? "";

            var counts = DbContext.Teams
                .Include(e => e.Players)
                .Where(e => teamName == "" || e.TeamName.Contains(teamName))
                .Select(e => new { e.TeamKey, Count = e.Players.Count })
                .AsNoTracking()
                .ToList();

            var list = counts
                .GroupBy(
                    e => e.Count,
                    (key, grp) => new CountTeamsResultDao
                    {
                        ItemCount = key,
                        CountOfTeams = grp.Count()
                    })
                .OrderByDescending(o => o.ItemCount)
                .ToList();

            if (list.Count == 0)
                throw new CommandFailedException(DalText.CountTeams_CountFailed);

            return list;
        }

        #endregion
    }
}
