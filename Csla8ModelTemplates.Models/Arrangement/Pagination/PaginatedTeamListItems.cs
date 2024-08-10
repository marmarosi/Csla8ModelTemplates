using Csla;
using Csla8ModelTemplates.Contracts.Arrangement.Pagination;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Arrangement.Pagination
{
    /// <summary>
    /// Represents a page of read-only team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamListItems : ReadOnlyList<PaginatedTeamListItems, PaginatedTeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PaginatedTeamListItems),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private void Fetch(
            List<PaginatedTeamListItemDao> list,
            [Inject] IChildDataPortal<PaginatedTeamListItem> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(itemPortal.FetchChild(item));
        }

        #endregion
    }
}
