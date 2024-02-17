using Csla8ModelTemplates.Contracts.Selection.WithCode;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.MySql.Selection.WithCode
{
    /// <summary>
    /// Implements the data access functions of the read-only team choice collection.
    /// </summary>
    [DalImplementation]
    public class TeamCodeChoiceDal : DalBase<MySqlContext>, ITeamCodeChoiceDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamCodeChoiceDal(
            MySqlContext dbContext
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
        public List<CodeNameOptionDao> Fetch(
            TeamCodeChoiceCriteria criteria
            )
        {
            var choice = DbContext.Teams
                .Where(e =>
                    criteria.TeamName == null || e.TeamName.Contains(criteria.TeamName)
                )
                .Select(e => new CodeNameOptionDao
                {
                    Code = e.TeamCode,
                    Name = e.TeamName
                })
                .OrderBy(o => o.Name)
                .AsNoTracking()
                .ToList();

            return choice;
        }

        #endregion GetChoice
    }
}
