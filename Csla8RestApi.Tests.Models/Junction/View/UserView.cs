using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Junction.View;

namespace Csla8RestApi.Tests.Models.Junction.View
{
    /// <summary>
    /// Represents a read-only user object.
    /// </summary>
    [Serializable]
    public class UserView : ReadOnlyModel<UserView>
    {
        #region Properties

        public static readonly PropertyInfo<long?> UserKeyProperty = RegisterProperty<long?>(nameof(UserKey));
        public long? UserKey
        {
            get => GetProperty(UserKeyProperty);
            private set => LoadProperty(UserKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> UserIdProperty = RegisterProperty<string?>(nameof(UserId), RelationshipTypes.PrivateField);
        public string? UserId
        {
            get => KeyHash.Encode(ID.User, UserKey);
            private set => UserKey = KeyHash.Decode(ID.User, value);
        }

        public static readonly PropertyInfo<string?> UserCodeProperty = RegisterProperty<string?>(nameof(UserCode));
        public string? UserCode
        {
            get => GetProperty(UserCodeProperty);
            private set => LoadProperty(UserCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> UserNameProperty = RegisterProperty<string?>(nameof(UserName));
        public string? UserName
        {
            get => GetProperty(UserNameProperty);
            private set => LoadProperty(UserNameProperty, value);
        }

        public static readonly PropertyInfo<UserViewRoles> RolesProperty = RegisterProperty<UserViewRoles>(nameof(Roles));
        public UserViewRoles Roles
        {
            get => GetProperty(RolesProperty);
            private set => LoadProperty(RolesProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            UserNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(UserView),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified read-only user instance.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="userId">The identifier of the user.</param>
        /// <returns>The requested read-only user instance.</returns>
        public static async Task<UserView> GetAsync(
            IDataPortalFactory factory,
            string userId
            )
        {
            return await factory.GetPortal<UserView>().FetchAsync(new UserViewCriteria(userId));
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            UserViewCriteria criteria,
            [Inject] IUserViewDal dal,
            [Inject] IChildDataPortal<UserViewRoles> itemsPortal
            )
        {
            // Load values from persistent storage.
            UserViewDao dao = await dal.FetchAsync(criteria);
            DataMapper.Map(dao, this, "Roles");
            Roles = await itemsPortal.FetchChildAsync(dao.Roles);
        }

        #endregion
    }
}
