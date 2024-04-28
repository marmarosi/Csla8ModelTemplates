using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Junction.Edit;

namespace Csla8RestApi.Tests.Models.Junction.Edit
{
    /// <summary>
    /// Represents an editable user-role collection.
    /// </summary>
    [Serializable]
    public class UserRoles : EditableList<UserRoles, UserRole, UserRoleDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(UserRoles),
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
            List<UserRoleDao> list,
            [Inject] IChildDataPortal<UserRole> itemPortal
            )
        {
            foreach (var item in list)
                Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
