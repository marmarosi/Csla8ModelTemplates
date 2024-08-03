using Csla8ModelTemplates.Contracts.Selection.ByKey;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Oracle.Selection.ByKey
{
    /// <summary>
    /// Implements the data access functions of the read-only team choice collection.
    /// </summary>
    [DalImplementation]
    public class TeamByKeyChoiceDal : DalBase<OracleContext>, ITeamByKeyChoiceDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamByKeyChoiceDal(
            OracleContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the choice of the teams.
        /// </summary>
        /// <param name="criteria">The criteria of the team choice.</param>
        /// <returns>The data transfer object of the requested team choice.</returns>
        public async Task<List<ChoiceItemDao<long?>>> FetchAsync(
            TeamByKeyChoiceCriteria criteria
            )
        {
            var choice = await DbContext.Teams
                .Where(e => criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName))
                .Select(e => new ChoiceItemDao<long?>
                {
                    Value = e.TeamKey,
                    Name = e.TeamName
                })
                .OrderBy(o => o.Name)
                .AsNoTracking()
                .ToListAsync();

            return choice;
        }

        #endregion GetChoice
    }
}
