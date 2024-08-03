using Csla8ModelTemplates.Contracts.Simple.Command;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Db2.Simple.Command
{
    /// <summary>
    /// Implements the data access functions of the rename team command.
    /// </summary>
    [DalImplementation]
    public class RenameTeamDal : DalBase<Db2Context>, IRenameTeamDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RenameTeamDal(
            Db2Context dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Execute

        /// <summary>
        /// Executes the rename team command.
        /// </summary>
        /// <param name="dao">The data of the command.</param>
        public async Task ExecuteAsync(
            RenameTeamDao dao
            )
        {
            // Get the specified team.
            var team = await DbContext.Teams
                .Where(e => e.TeamKey == dao.TeamKey)
                .FirstOrDefaultAsync()
                ?? throw new DataNotFoundException(SimpleText.SimpleTeam_NotFound);

            // Update the team.
            team.TeamName = dao.TeamName;

            int count = await DbContext.SaveChangesAsync();
            if (count == 0)
                throw new CommandFailedException(SimpleText.RenameTeam_Failed);

            // Signal successful completion.
            dao.Result = true;
        }

        #endregion
    }
}
