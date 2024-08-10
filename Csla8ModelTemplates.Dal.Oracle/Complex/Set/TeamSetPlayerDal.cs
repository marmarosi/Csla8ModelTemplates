using Csla8ModelTemplates.Contracts.Complex.Set;
using Csla8ModelTemplates.Entities;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Oracle.Complex.Set
{
    /// <summary>
    /// Implements the data access functions of the editable player object.
    /// </summary>
    [DalImplementation]
    public class TeamSetPlayerDal : DalBase<OracleContext>, ITeamSetPlayerDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamSetPlayerDal(
            OracleContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Insert

        /// <summary>
        /// Creates a new player using the specified data.
        /// </summary>
        /// <param name="dao">The data of the player.</param>
        public async Task InsertAsync(
            TeamSetPlayerDao dao
            )
        {
            // Check unique player code.
            var player = await DbContext.Players
                .Where(e =>
                    e.TeamKey == dao.TeamKey &&
                    e.PlayerCode == dao.PlayerCode
                )
                .FirstOrDefaultAsync();
            if (player is not null)
                throw new DataExistException(ComplexText.TeamSetPlayer_PlayerCodeExists
                    .With(dao.__teamCode!, dao.PlayerCode!));

            // Create the new player.
            player = new Player
            {
                TeamKey = dao.TeamKey,
                PlayerCode = dao.PlayerCode,
                PlayerName = dao.PlayerName
            };
            await DbContext.Players.AddAsync(player);

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new InsertFailedException(ComplexText.TeamSetPlayer_InsertFailed
                    .With(dao.__teamCode!, dao.PlayerCode!));

            // Return new data.
            dao.PlayerKey = player.PlayerKey;
        }

        #endregion Insert

        #region Update

        /// <summary>
        /// Updates an existing player using the specified data.
        /// </summary>
        /// <param name="dao">The data of the player.</param>
        public async Task UpdateAsync(
            TeamSetPlayerDao dao
            )
        {
            // Get the specified player.
            var player = await DbContext.Players
                .Where(e =>
                    e.PlayerKey == dao.PlayerKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.TeamSetPlayer_NotFound
                    .With(dao.__teamCode!, dao.PlayerCode!));

            // Check unique player code.
            if (player.PlayerCode != dao.PlayerCode)
            {
                int exist = await DbContext.Players
                    .Where(e =>
                        e.TeamKey == dao.TeamKey &&
                        e.PlayerCode == dao.PlayerCode &&
                        e.PlayerKey != player.PlayerKey
                    )
                    .CountAsync();
                if (exist > 0)
                    throw new DataExistException(ComplexText.TeamSetPlayer_PlayerCodeExists
                        .With(dao.__teamCode!, dao.PlayerCode!));
            }

            // Update the player.
            player.PlayerCode = dao.PlayerCode;
            player.PlayerName = dao.PlayerName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(ComplexText.TeamSetPlayer_UpdateFailed
                    .With(dao.__teamCode!, dao.PlayerCode!));

            // Return new data.
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified player.
        /// </summary>
        /// <param name="criteria">The criteria of the player.</param>
        public async Task DeleteAsync(
            TeamSetPlayerCriteria criteria
            )
        {
            int count = 0;

            // Get the specified player.
            var player = await DbContext.Players
                .Where(e =>
                    e.PlayerKey == criteria.PlayerKey
                 )
                .AsNoTracking()
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.TeamSetPlayer_NotFound
                    .With(criteria.__teamCode!, criteria.__playerCode!));

            // Delete the player.
            DbContext.Players.Remove(player);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(ComplexText.TeamSetPlayer_DeleteFailed
                    .With(criteria.__teamCode!, criteria.__playerCode!));
        }

        #endregion Delete
    }
}
