using Csla;
using Csla8ModelTemplates.Contracts.Junction.View;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Junction.View
{
    /// <summary>
    /// Represents a read-only group-person collection.
    /// </summary>
    [Serializable]
    public class GroupViewPersons : ReadOnlyList<GroupViewPersons, GroupViewPerson>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPersonViews),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private async Task FetchAsync(
            List<GroupViewPersonDao> list,
            [Inject] IChildDataPortal<GroupViewPerson> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
