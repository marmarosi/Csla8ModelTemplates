using Csla8ModelTemplates.Contracts.Simple.Set;
using Csla8ModelTemplates.Entities;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Firebird.Simple.Set
{
    /// <summary>
    /// Implements the data access functions of the editable team set item object.
    /// </summary>
    [DalImplementation]
    public class SimpleTeamSetItemDal : DalBase<FirebirdContext>, ISimpleTeamSetItemDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public SimpleTeamSetItemDal(
            FirebirdContext dbContext
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
            SimpleTeamSetItemDao dao
            )
        {
            // Check unique team code.
            var team = await DbContext.Teams
                .Where(e =>
                    e.TeamCode == dao.TeamCode
                )
                .FirstOrDefaultAsync();
            if (team is not null)
                throw new DataExistException(SimpleText.SimpleTeamSetItem_TeamCodeExists.With(dao.TeamCode!));

            // Create the new team.
            team = new Team
            {
                TeamCode = dao.TeamCode,
                TeamName = dao.TeamName
            };
            await DbContext.Teams.AddAsync(team);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(SimpleText.SimpleTeamSetItem_InsertFailed.With(team.TeamCode!));

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
            SimpleTeamSetItemDao dao
            )
        {
            // Get the specified team.
            var team = await DbContext.Teams
                .Where(e =>
                    e.TeamKey == dao.TeamKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.SimpleTeamSetItem_NotFound.With(dao.TeamCode!));
            if (team.Timestamp != dao.Timestamp)
                throw new ConcurrencyException(SimpleText.SimpleTeamSetItem_Concurrency.With(dao.TeamCode!));

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
                    throw new DataExistException(SimpleText.SimpleTeamSetItem_TeamCodeExists.With(dao.TeamCode!));
            }

            // Update the team.
            team.TeamCode = dao.TeamCode;
            team.TeamName = dao.TeamName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(SimpleText.SimpleTeamSetItem_UpdateFailed.With(team.TeamCode!));

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
            SimpleTeamSetItemCriteria criteria
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
                ?? throw new DataNotFoundException(SimpleText.SimpleTeamSetItem_NotFoundKey);

            // Check references.
            //int dependents = 0;

            //dependents = await DbContext.Others.CountAsync(e => e.TeamKey == criteria.TeamKey);
            //if (dependents > 0)
            //    throw new DeleteFailedException(SimpleText.SimpleTeamSetItem_Delete_Others);

            // Delete references.
            var players = await DbContext.Players
                .Where(e => e.TeamKey == criteria.TeamKey)
                .ToListAsync();
            foreach (var player in players)
                DbContext.Players.Remove(player);

            count = await DbContext.SaveChangesAsync();
            if (count != players.Count)
                throw new DeleteFailedException(SimpleText.SimpleTeamSetItem_Delete_Players.With(team.TeamCode!));

            // Delete the team.
            DbContext.Teams.Remove(team);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(SimpleText.SimpleTeamSetItem_DeleteFailed.With(team.TeamCode!));
        }

        #endregion Delete
    }
}
