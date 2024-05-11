using Csla8ModelTemplates.Contracts.Simple.View;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.PostgreSql.Simple.View
{
    /// <summary>
    /// Implements the data access functions of the read-only team model.
    /// </summary>
    [DalImplementation]
    public partial class SimpleTeamViewDal : DalBase<PostgreSqlContext>, ISimpleTeamViewDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SimpleTeamViewDal(
            PostgreSqlContext dbContext
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
        public async Task<SimpleTeamViewDao> FetchAsync(
            SimpleTeamViewCriteria criteria
            )
        {
            // Get the specified team.
            var team = await DbContext.Teams
                .Where(e =>
                    e.TeamKey == criteria.TeamKey
                 )
                .Select(e => new SimpleTeamViewDao
                {
                    TeamKey = e.TeamKey,
                    TeamCode = e.TeamCode,
                    TeamName = e.TeamName
                })
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.SimpleTeam_NotFound);

            return team;
        }

        #endregion Fetch
    }
}
