using Csla8RestApi.Dal;
using Csla8RestApi.Tests.Contracts.Tree.View;
using Microsoft.EntityFrameworkCore;

namespace Csla8RestApi.Tests.Dal.Rdbms.Tree.View
{
    /// <summary>
    /// Implements the data access functions of the read-only message tree.
    /// </summary>
    [DalImplementation]
    public class MessageTreeDal : DalBase<RdbmsContext>, IMessageTreeDal
    {
        #region Constructor

        /// <summary>
        /// Instantiates the data access object.
        /// </summary>
        /// <param name="context">The database context.</param>
        public MessageTreeDal(
            RdbmsContext dbContext
            )
        {
            DbContext = dbContext;
        }

        #endregion Constructor

        #region Fetch

        private List<MessageNodeDao>? AllMessages { get; set; }

        /// <summary>
        /// Gets the specified message tree.
        /// </summary>
        /// <param name="criteria">The criteria of the message tree.</param>
        /// <returns>The requested message tree.</returns>
        public async Task<List<MessageNodeDao>> FetchAsync(
            MessageTreeCriteria criteria
            )
        {
            var tree = new List<MessageNodeDao>();

            // Get all subfolders of the root foolder.
            AllMessages = await DbContext.Messages
                .Where(e =>
                    e.RootKey == criteria.RootKey
                )
                .Select(e => new MessageNodeDao
                {
                    MessageKey = e.MessageKey,
                    ParentKey = e.ParentKey,
                    MessageOrder = e.MessageOrder,
                    MessageName = e.MessageName
                })
                .AsNoTracking()
                .ToListAsync();

            // Populate the tree.
            PopulateLevel(1, null, tree);

            // Return the result.
            return tree;
        }

        private void PopulateLevel(
            int level,
            long? parentKey,
            List<MessageNodeDao> parentChildren
            )
        {
            // Get the folders of the level.
            var folders = AllMessages!
                .Where(o => o.ParentKey == parentKey)
                .OrderBy(o => o.MessageOrder)
                .ToList();

            foreach (MessageNodeDao folder in folders)
            {
                // Create folder node.
                MessageNodeDao folderNode = new MessageNodeDao
                {
                    MessageKey = folder.MessageKey,
                    ParentKey = folder.ParentKey,
                    MessageOrder = folder.MessageOrder,
                    MessageName = folder.MessageName,
                    Level = level,
                    Children = new List<MessageNodeDao>()
                };

                // Add folder to the parent's children.
                parentChildren.Add(folderNode);

                // Get the subfolders of this folder.
                PopulateLevel(
                    level + 1,
                    folder.MessageKey,
                    folderNode.Children
                    );
            }
        }

        #endregion GetList
    }
}
