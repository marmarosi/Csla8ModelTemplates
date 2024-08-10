using Csla;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Tree.Choice;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Tree.Choice
{
    /// <summary>
    /// Represents a read-only tree choice collection.
    /// </summary>
    [Serializable]
    public class RootFolderChoice : ReadOnlyList<RootFolderChoice, ChoiceItem<string?>>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamIdChoice),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a choice of tree options.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <returns>The requested tree choice instance.</returns>
        public static async Task<RootFolderChoice> GetAsync(
            IDataPortalFactory factory
            )
        {
            var criteria = new RootFolderChoiceCriteria();
            return await factory.GetPortal<RootFolderChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            RootFolderChoiceCriteria criteria,
            [Inject] IRootFolderChoiceDal dal,
            [Inject] IChildDataPortal<ChoiceItem<string?>> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ChoiceItemDao<long?>> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item.ToId(ID.Folder)));
            }
        }

        #endregion
    }
}
