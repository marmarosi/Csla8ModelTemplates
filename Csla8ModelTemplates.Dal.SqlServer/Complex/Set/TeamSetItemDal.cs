using Csla8ModelTemplates.Contracts.Complex.Set;
using Csla8ModelTemplates.Entities;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.SqlServer.Complex.Set
{
    /// <summary>
    /// Implements the data access functions of the editable team set item object.
    /// </summary>
    [DalImplementation]
    public class TeamSetItemDal : DalBase<SqlServerContext>, ITeamSetItemDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamSetItemDal(
            SqlServerContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Insert

        /// <summary>
        /// Creates a new team using the specified data.
        /// </summary>
        /// <param name="dao">The data of the team.</param>
        public async Task InsertAsync(
            TeamSetItemDao dao
            )
        {
            // Check unique team code.
            var team = await DbContext.Teams
                .Where(e =>
                    e.TeamCode == dao.TeamCode
                )
                .FirstOrDefaultAsync();
            if (team is not null)
                throw new DataExistException(ComplexText.TeamSetItem_TeamCodeExists.With(dao.TeamCode!));

            // Create the new team.
            team = new Team
            {
                TeamCode = dao.TeamCode,
                TeamName = dao.TeamName
            };
            await DbContext.Teams.AddAsync(team);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(ComplexText.TeamSetItem_InsertFailed.With(team.TeamCode!));

            // Return new data.
            dao.TeamKey = team.TeamKey;
            dao.Timestamp = team.Timestamp;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing team using the specified data.
        /// </summary>
        /// <param name="dao">The data of the team.</param>
        public async Task UpdateAsync(
            TeamSetItemDao dao
            )
        {
            // Get the specified team.
            var team = await DbContext.Teams
                .Where(e =>
                    e.TeamKey == dao.TeamKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.TeamSetItem_NotFound.With(dao.TeamCode!));
            if (team.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(ComplexText.TeamSetItem_Concurrency.With(dao.TeamCode!));

            // Check unique team code.
            if (team.TeamCode != dao.TeamCode)
            {
                int exist = await DbContext.Teams
                    .Where(e =>
                        e.TeamCode == dao.TeamCode &&
                        e.TeamKey != team.TeamKey
                    )
                    .CountAsync();
                if (exist > 0)
                    throw new DataExistException(ComplexText.TeamSetItem_TeamCodeExists.With(dao.TeamCode!));
            }

            // Update the team.
            team.TeamCode = dao.TeamCode;
            team.TeamName = dao.TeamName;
            team.Timestamp = DateTime.Now; // Force update timestamp.

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(ComplexText.TeamSetItem_UpdateFailed.With(team.TeamCode!));

            // Return new data.
            dao.Timestamp = team.Timestamp;
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified team.
        /// </summary>
        /// <param name="criteria">The criteria of the team.</param>
        public async Task DeleteAsync(
            TeamSetItemCriteria criteria
            )
        {
            int count = 0;

            // Get the specified team.
            var team = await DbContext.Teams
                .Where(e =>
                    e.TeamKey == criteria.TeamKey
                 )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.TeamSetItem_NotFoundKey);

            // Check references.
            //int dependents = 0;

            //dependents = await DbContext.Others.CountAsync(e => e.TeamKey == criteria.TeamKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(ComplexText.TeamSetItem_Delete_Others);

            // Delete references.

            // Delete the team.
            DbContext.Teams.Remove(team);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(ComplexText.TeamSetItem_DeleteFailed.With(team.TeamCode!));
        }

        #endregion Delete
    }
}
