using Csla8ModelTemplates.Contracts.Tree.Choice;
using Csla8RestApi.Dal;
using Csla8RestApi.Dal.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Csla8ModelTemplates.Dal.Sqlite.Tree.Choice
{
    /// <summary>
    /// Implements the data access functions of the read-only tree choice collection.
    /// </summary>
    [DalImplementation]
    public class RootFolderChoiceDal : DalBase<SqliteContext>, IRootFolderChoiceDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public RootFolderChoiceDal(
            SqliteContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        /// <summary>
        /// Gets the choice of the trees.
        /// </summary>
        /// <param name="criteria">The criteria of the tree choice.</param>
        /// <returns>The data transfer object of the requested tree choice.</returns>
        public async Task<List<ChoiceItemDao<long?>>> FetchAsync(
            RootFolderChoiceCriteria criteria
            )
        {
            var choice = await DbContext.Folders
                .Where(e => e.ParentKey == null)
                .Select(e => new ChoiceItemDao<long?>
                {
                    Value = e.FolderKey,
                    Name = e.FolderName
                })
                .OrderBy(o => o.Name)
                .AsNoTracking()
                .ToListAsync();

            return choice;
        }

        #endregion GetChoice
    }
}
