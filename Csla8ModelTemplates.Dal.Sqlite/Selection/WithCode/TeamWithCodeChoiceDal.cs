using Csla8ModelTemplates.Contracts.Selection.WithCode;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Sqlite.Selection.WithCode
{
    /// <summary>
    /// Implements the data access functions of the read-only team choice collection.
    /// </summary>
    [DalImplementation]
    public class TeamWithCodeChoiceDal : DalBase<SqliteContext>, ITeamWithCodeChoiceDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamWithCodeChoiceDal(
            SqliteContext dbContext
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
        public async Task<List<ChoiceItemDao<string?>>> FetchAsync(
            TeamWithCodeChoiceCriteria criteria
            )
        {
            var choice = await DbContext.Teams
                .Where(e =>
                    criteria.TeamName == null || e.TeamName!.Contains(criteria.TeamName)
                )
                .Select(e => new ChoiceItemDao<string?>
                {
                    Value = e.TeamCode,
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
