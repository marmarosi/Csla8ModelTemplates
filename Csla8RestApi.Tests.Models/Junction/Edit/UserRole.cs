using Csla;
using Csla.Core;
using Csla.Data;
using Csla.Rules;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Junction.Edit;

namespace Csla8RestApi.Tests.Models.Junction.Edit
{
    /// <summary>
    /// Represents an editable user-role object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(JunctionText), ObjectName = "UserRole")]
    public class UserRole : EditableModel<UserRole, UserRoleDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> RoleKeyProperty = RegisterProperty<long?>(nameof(RoleKey));
        public long? RoleKey
        {
            get => GetProperty(RoleKeyProperty);
            private set => LoadProperty(RoleKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> RoleIdProperty = RegisterProperty<string?>(nameof(RoleId), RelationshipTypes.PrivateField);
        public string? RoleId
        {
            get => KeyHash.Encode(ID.Role, RoleKey);
            set => RoleKey = KeyHash.Decode(ID.Role, value);
        }

        public static readonly PropertyInfo<string?> RoleNameProperty = RegisterProperty<string?>(nameof(RoleName));
        public string? RoleName
        {
            get => GetProperty(RoleNameProperty);
            private set => LoadProperty(RoleNameProperty, value);
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            //// Add validation rules.
            BusinessRules.AddRule(new UniqueRoleIds(RoleIdProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(
            //    new IsInRole(
            //        AuthorizationActions.WriteProperty,
            //        RoleNameProperty,
            //        "Manager"
            //        )
            //    );
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(UserRole),
        //        new IsInRole(
        //            AuthorizationActions.EditObject,
        //            "Manager"
        //            )
        //        );
        //}

        private sealed class UniqueRoleIds : BusinessRule
        {
            // Add additional parameters to your rule to the constructor.
            public UniqueRoleIds(
                IPropertyInfo primaryProperty
                )
              : base(primaryProperty)
            {
                // If you are  going to add InputProperties make sure to
                // uncomment line below as InputProperties is NULL by default.
                //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

                // Add additional constructor code here.

                // Marke rule for IsAsync if Execute contains asyncronous code IsAsync = true; 
            }

            protected override void Execute(
                IRuleContext context
                )
            {
                UserRole target = (UserRole)context.Target;
                if (target.Parent == null)
                    return;

                User user = (User)target.Parent.Parent;
                var count = user.Roles.Count(gp => gp.RoleId == target.RoleId);
                if (count > 1)
                    context.AddErrorResult(JunctionText.UserRole_RoleId_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public override async Task SetValuesOnBuild(
            UserRoleDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this);
            await BusinessRules.CheckRulesAsync();
        }

        #endregion

        #region Data Access

        [CreateChild]
        private async Task CreateAsync()
        {
            // Set values from data transfer object.
            await Task.Run(async () =>
            {
                //LoadProperty(RoleNameProperty, "");
                await BusinessRules.CheckRulesAsync();
            });
        }

        [FetchChild]
        private async Task FetchAsync(
            UserRoleDao dao
            )
        {
            // Load values from persistent storage.
            await Task.Run(() =>
            {
                using (BypassPropertyChecks)
                    DataMapper.Map(dao, this, "UserKey");
            });
        }

        [InsertChild]
        private async Task InsertAsync(
            User parent,
            [Inject] IUserRoleDal dal
            )
        {
            // Insert values into persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).ToNew<UserRoleDao>();
                dao.UserKey = parent.UserKey;
                await dal.InsertAsync(dao);

                // Set new data.
                RoleKey = dao.RoleKey;
            }
            //await FieldManager.UpdateChildrenAsync(this);
        }

        //[UpdateChild]
        //private async Task UpdateAsync(
        //    User parent,
        //    [Inject] IUserRoleDal dal
        //    )
        //{
        //    // Update values in persistent storage.
        //    throw new NotImplementedException();
        //}

        [DeleteSelfChild]
        private async Task Child_DeleteSelfAsync(
            User parent,
            [Inject] IUserRoleDal dal
            )
        {
            // Delete values from persistent storage.

            //Items.Clear();
            //FieldManager.UpdateChildren(this);

            UserRoleDao dao = Copy.PropertiesFrom(this).Omit("RoleId").ToNew<UserRoleDao>();
            dao.UserKey = parent.UserKey;
            await dal.DeleteAsync(dao);
        }

        #endregion
    }
}
