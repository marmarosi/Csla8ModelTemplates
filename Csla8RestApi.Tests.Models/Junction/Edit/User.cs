using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Junction.Edit;

namespace Csla8RestApi.Tests.Models.Junction.Edit
{
    /// <summary>
    /// Represents an editable user object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(JunctionText), ObjectName = "User")]
    public class User : EditableModel<User, UserDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> UserKeyProperty = RegisterProperty<long?>(nameof(UserKey));
        public long? UserKey
        {
            get => GetProperty(UserKeyProperty);
            private set => SetProperty(UserKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> UserIdProperty = RegisterProperty<string?>(nameof(UserId), RelationshipTypes.PrivateField);
        public string? UserId
        {
            get => KeyHash.Encode(ID.User, UserKey);
            set => UserKey = KeyHash.Decode(ID.User, value);
        }

        public static readonly PropertyInfo<string?> UserCodeProperty = RegisterProperty<string?>(nameof(UserCode));
        [Required]
        [MaxLength(10)]
        public string? UserCode
        {
            get => GetProperty(UserCodeProperty);
            set => SetProperty(UserCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> UserNameProperty = RegisterProperty<string?>(nameof(UserName));
        [Required]
        [MaxLength(100)]
        public string? UserName
        {
            get => GetProperty(UserNameProperty);
            set => SetProperty(UserNameProperty, value);
        }

        public static readonly PropertyInfo<UserRoles> UserRolesProperty = RegisterProperty<UserRoles>(nameof(Roles));
        public UserRoles Roles
        {
            get => GetProperty(UserRolesProperty);
            private set => LoadProperty(UserRolesProperty, value);
        }

        public static readonly PropertyInfo<DateTimeOffset?> TimestampProperty = RegisterProperty<DateTimeOffset?>(nameof(Timestamp));
        public DateTimeOffset? Timestamp
        {
            get => GetProperty(TimestampProperty);
            private set => LoadProperty(TimestampProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Call base class implementation to add data annotation rules to BusinessRules.
        //    // NOTE: DataAnnotation rules is always added with Priority = 0.
        //    base.AddBusinessRules();

        //    // Add validation rules.
        //    BusinessRules.AddRule(new Required(UserNameProperty));

        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.WriteProperty,
        //            UserNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(User),
        //        new IsInRole(
        //            AuthorizationActions.EditObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public override async Task SetValuesOnBuild(
            UserDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this, "Roles");
            await BusinessRules.CheckRulesAsync();
            await Roles.SetValuesById(dto.Roles, "RoleId", childFactory);
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a new user to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <returns>The new user.</returns>
        public static async Task<User> NewAsync(
            IDataPortalFactory factory
            )
        {
            return await factory.GetPortal<User>().CreateAsync();
        }

        /// <summary>
        /// Gets an existing editable user instance.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="id">The identifier of the user.</param>
        /// <returns>The requested editable user instance.</returns>
        public static async Task<User> GetAsync(
            IDataPortalFactory factory,
            string id
            )
        {
            var criteria = new UserParams(id);
            return await factory.GetPortal<User>().FetchAsync(criteria.Decode());
        }

        /// <summary>
        /// Builds a new or existing user.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="dto"></param>
        /// <returns>The user built.</returns>
        public static async Task<User> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            UserDto dto
            )
        {
            long? userKey = KeyHash.Decode(ID.User, dto.UserId);
            User user = userKey.HasValue ?
                await GetAsync(factory, dto.UserId!) :
                await NewAsync(factory);
            await user.SetValuesOnBuild(dto, childFactory);
            return user;
        }

        /// <summary>
        /// Deletes an existing user.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="id">The identifier of the user.</param>
        public static async Task DeleteAsync(
            IDataPortalFactory factory,
            string id
            )
        {
            var criteria = new UserParams(id);
            await factory.GetPortal<User>().DeleteAsync(criteria.Decode());
        }

        #endregion

        #region Data Access

        [Create]
        [RunLocal]
        private async Task CreateAsync(
            [Inject] IChildDataPortal<UserRoles> itemsPortal
            )
        {
            // Load default values.
            //LoadProperty(UserCodeProperty, "");
            Roles = await itemsPortal.CreateChildAsync();
            await BusinessRules.CheckRulesAsync();
        }

        [Fetch]
        private async Task FetchAsync(
            UserCriteria criteria,
            [Inject] IUserDal dal,
            [Inject] IChildDataPortal<UserRoles> itemsPortal
            )
        {
            // Load values from persistent storage.
            UserDao dao = await dal.FetchAsync(criteria);
            using (BypassPropertyChecks)
            {
                DataMapper.Map(dao, this, "Roles");
                Roles = await itemsPortal.FetchChildAsync(dao.Roles);
            }
        }

        [Insert]
        protected async Task InsertAsync(
            [Inject] IUserDal dal
            )
        {
            // Insert values into persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).Omit("Roles").ToNew<UserDao>();
                    await dal.InsertAsync(dao);

                    // Set new data.
                    UserKey = dao.UserKey;
                    Timestamp = dao.Timestamp;
                }
                await FieldManager.UpdateChildrenAsync(this);
                dal.Commit(transaction);
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] IUserDal dal
            )
        {
            // Update values in persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                if (IsSelfDirty)
                {
                    using (BypassPropertyChecks)
                    {
                        var dao = Copy.PropertiesFrom(this).Omit("Roles").ToNew<UserDao>();
                        await dal.UpdateAsync(dao);

                        // Set new data.
                        Timestamp = dao.Timestamp;
                    }
                }
                await FieldManager.UpdateChildrenAsync(this);
                dal.Commit(transaction);
            }
        }

        [DeleteSelf]
        protected async Task DeleteSelfAsync(
            [Inject] IUserDal dal,
            [Inject] IChildDataPortal<UserRoles> itemPortal
            )
        {
            using (BypassPropertyChecks)
                await DeleteAsync(new UserCriteria(UserKey), dal, itemPortal);
        }

        [Delete]
        protected async Task DeleteAsync(
            UserCriteria criteria,
            [Inject] IUserDal dal,
            [Inject] IChildDataPortal<UserRoles> itemPortal
            )
        {
            // Delete values from persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                if (!UserKey.HasValue)
                    await FetchAsync(criteria, dal, itemPortal);

                Roles.Clear();
                await FieldManager.UpdateChildrenAsync(this);

                await dal.DeleteAsync(criteria);
                dal.Commit(transaction);
            }
        }

        #endregion
    }
}
