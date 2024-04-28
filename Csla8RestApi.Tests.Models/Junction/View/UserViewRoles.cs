using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Junction.View;

namespace Csla8RestApi.Tests.Models.Junction.View
{
    /// <summary>
    /// Represents a read-only user-role collection.
    /// </summary>
    [Serializable]
    public class UserViewRoles : ReadOnlyList<UserViewRoles, UserViewRole>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(UserRoleViews),
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
            List<UserViewRoleDao> list,
            [Inject] IChildDataPortal<UserViewRole> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
