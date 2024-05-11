using Csla8ModelTemplates.Contracts.Complex.Edit;
using Csla8RestApi.Dal.Exceptions;
using Csla8ModelTemplates.Entities;
using Csla8RestApi.Dal;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Oracle.Complex.Edit
{
    /// <summary>
    /// Implements the data access functions of the editable player object.
    /// </summary>
    [DalImplementation]
    public class TeamPlayerDal : DalBase<OracleContext>, ITeamPlayerDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public TeamPlayerDal(
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
            TeamPlayerDao dao
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
                throw new DataExistException(ComplexText.Player_PlayerCodeExists.With(dao.PlayerCode!));

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
                throw new InsertFailedException(ComplexText.Player_InsertFailed.With(player.PlayerCode!));

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
            TeamPlayerDao dao
            )
        {
            // Get the specified player.
            var player = await DbContext.Players
                .Where(e =>
                    e.PlayerKey == dao.PlayerKey
                )
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(ComplexText.Player_NotFound);

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
                    throw new DataExistException(ComplexText.Player_PlayerCodeExists.With(dao.PlayerCode!));
            }

            // Update the player.
            player.PlayerCode = dao.PlayerCode;
            player.PlayerName = dao.PlayerName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new UpdateFailedException(ComplexText.Player_UpdateFailed.With(player.PlayerCode!));

            // Return new data.
        }

        #endregion Update

        #region Delete

        /// <summary>
        /// Deletes the specified player.
        /// </summary>
        /// <param name="criteria">The criteria of the player.</param>
        public async Task DeleteAsync(
            TeamPlayerCriteria criteria
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
                ?? throw new DataNotFoundException(ComplexText.Player_NotFound);

            // Delete the player.
            DbContext.Players.Remove(player);

            count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new DeleteFailedException(ComplexText.Player_DeleteFailed.With(player.PlayerCode!));
        }

        #endregion Delete
    }
}
