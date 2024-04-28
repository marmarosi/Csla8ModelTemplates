using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Junction.View;

namespace Csla8RestApi.Tests.Models.Junction.View
{
    /// <summary>
    /// Represents an item in a read-only user-role collection.
    /// </summary>
    [Serializable]
    public class UserViewRole : ReadOnlyModel<UserViewRole>
    {
        #region Properties

        public static readonly PropertyInfo<long?> RoleKeyProperty = RegisterProperty<long?>(nameof(RoleKey));
        public long? RoleKey
        {
            get => GetProperty(RoleKeyProperty);
            private set => LoadProperty(RoleKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> RoleIdProperty = RegisterProperty<long?>(nameof(RoleId), RelationshipTypes.PrivateField);
        public string RoleId
        {
            get => KeyHash.Encode(ID.Role, RoleKey);
            private set => RoleKey = KeyHash.Decode(ID.Role, value);
        }

        public static readonly PropertyInfo<string> RoleNameProperty = RegisterProperty<string>(nameof(RoleName));
        public string RoleName
        {
            get => GetProperty(RoleNameProperty);
            private set => LoadProperty(RoleNameProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            RoleNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(UserRoleView),
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
            UserViewRoleDao dao
            )
        {
            // Load values from persistent storage.
            await Task.Run(() =>
            {
                DataMapper.Map(dao, this);
            });
        }

        #endregion
    }
}
