using Csla;
using Csla8ModelTemplates.Contracts.Tree.View;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Tree.View
{
    /// <summary>
    /// Represents a read-only folder tree.
    /// </summary>
    [Serializable]
    public class FolderTree : ReadOnlyList<FolderTree, FolderNode>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(FolderTree),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified read-only folder tree.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the read-only folder tree.</param>
        /// <returns>The requested read-only folder tree.</returns>
        public static async Task<FolderTree> GetAsync(
            IDataPortalFactory factory,
            string? rootId
            )
        {
            return await factory.GetPortal<FolderTree>().FetchAsync(new FolderTreeCriteria(rootId));
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            FolderTreeCriteria criteria,
            [Inject] IFolderTreeDal dal,
            [Inject] IChildDataPortal<FolderNode> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<FolderNodeDao> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
