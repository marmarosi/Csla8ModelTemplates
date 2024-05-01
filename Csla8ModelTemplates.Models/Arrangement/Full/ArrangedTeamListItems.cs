using Csla;
using Csla8ModelTemplates.Contracts.Arrangement.Full;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Arrangement.Full
{
    /// <summary>
    /// Represents a page of read-only paginated sorted team collection.
    /// </summary>
    [Serializable]
    public class ArrangedTeamListItems : ReadOnlyList<ArrangedTeamListItems, ArrangedTeamListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ArrangedTeamListItems),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private async void Fetch(
            List<ArrangedTeamListItemDao> list,
            [Inject] IChildDataPortal<ArrangedTeamListItem> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
