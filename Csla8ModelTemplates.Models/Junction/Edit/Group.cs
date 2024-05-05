using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Junction.Edit;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;

namespace Csla8ModelTemplates.Models.Junction.Edit
{
    /// <summary>
    /// Represents an editable group object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(JunctionText), ObjectName = "Group")]
    public class Group : EditableModel<Group, GroupDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> GroupKeyProperty = RegisterProperty<long?>(nameof(GroupKey));
        public long? GroupKey
        {
            get => GetProperty(GroupKeyProperty);
            private set => SetProperty(GroupKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> GroupIdProperty = RegisterProperty<string?>(nameof(GroupId), RelationshipTypes.PrivateField);
        public string? GroupId
        {
            get => KeyHash.Encode(ID.Group, GroupKey);
            set => GroupKey = KeyHash.Decode(ID.Group, value);
        }

        public static readonly PropertyInfo<string?> GroupCodeProperty = RegisterProperty<string?>(nameof(GroupCode));
        [Required]
        [MaxLength(10)]
        public string? GroupCode
        {
            get => GetProperty(GroupCodeProperty);
            set => SetProperty(GroupCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> GroupNameProperty = RegisterProperty<string?>(nameof(GroupName));
        [Required]
        [MaxLength(100)]
        public string? GroupName
        {
            get => GetProperty(GroupNameProperty);
            set => SetProperty(GroupNameProperty, value);
        }

        public static readonly PropertyInfo<GroupPersons> GroupPersonsProperty = RegisterProperty<GroupPersons>(nameof(Persons));
        public GroupPersons Persons
        {
            get => GetProperty(GroupPersonsProperty);
            private set => LoadProperty(GroupPersonsProperty, value);
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
        //    BusinessRules.AddRule(new Required(GroupNameProperty));

        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.WriteProperty,
        //            GroupNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Group),
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
            GroupDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this, "Persons");
            await Persons.SetValuesById(dto.Persons, "PersonId", childFactory);
            await BusinessRules.CheckRulesAsync();
            await WaitForIdle();
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a new group to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <returns>The new group.</returns>
        public static async Task<Group> NewAsync(
            IDataPortalFactory factory
            )
        {
            return await factory.GetPortal<Group>().CreateAsync();
        }

        /// <summary>
        /// Gets an existing editable group instance.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="groupId">The identifier of the group.</param>
        /// <returns>The requested editable group instance.</returns>
        public static async Task<Group> GetAsync(
            IDataPortalFactory factory,
            string groupId
            )
        {
            return await factory.GetPortal<Group>().FetchAsync(new GroupCriteria(groupId));
        }

        /// <summary>
        /// Builds a new or existing group.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="dto"></param>
        /// <returns>The group built.</returns>
        public static async Task<Group> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            GroupDto dto
            )
        {
            long? groupKey = KeyHash.Decode(ID.Group, dto.GroupId);
            Group group = groupKey.HasValue ?
                await GetAsync(factory, dto.GroupId!) :
                await NewAsync(factory);
            await group.SetValuesOnBuild(dto, childFactory);
            return group;
        }

        /// <summary>
        /// Deletes an existing group.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="groupId">The identifier of the group.</param>
        public static async Task DeleteAsync(
            IDataPortalFactory factory,
            string groupId
            )
        {
            await factory.GetPortal<Group>().DeleteAsync(groupId);
        }

        #endregion

        #region Data Access

        [Create]
        [RunLocal]
        private async Task CreateAsync(
            [Inject] IChildDataPortal<GroupPersons> itemsPortal
            )
        {
            // Load default values.
            //LoadProperty(GroupCodeProperty, "");
            Persons = await itemsPortal.CreateChildAsync();
            await BusinessRules.CheckRulesAsync();
            await WaitForIdle();
        }

        [Fetch]
        private async Task FetchAsync(
            GroupCriteria criteria,
            [Inject] IGroupDal dal,
            [Inject] IChildDataPortal<GroupPersons> itemsPortal
            )
        {
            // Load values from persistent storage.
            GroupDao dao = await dal.FetchAsync(criteria);
            using (BypassPropertyChecks)
            {
                DataMapper.Map(dao, this, "Persons");
                Persons = await itemsPortal.FetchChildAsync(dao.Persons);
            }
        }

        [Insert]
        protected async Task InsertAsync(
            [Inject] IGroupDal dal
            )
        {
            // Insert values into persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).Omit("Persons").ToNew<GroupDao>();
                    await dal.InsertAsync(dao);

                    // Set new data.
                    GroupKey = dao.GroupKey;
                    Timestamp = dao.Timestamp;
                }
                await FieldManager.UpdateChildrenAsync(this);
                dal.Commit(transaction);
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] IGroupDal dal
            )
        {
            // Update values in persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                if (IsSelfDirty)
                {
                    using (BypassPropertyChecks)
                    {
                        var dao = Copy.PropertiesFrom(this).Omit("Persons").ToNew<GroupDao>();
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
            [Inject] IGroupDal dal,
            [Inject] IChildDataPortal<GroupPersons> itemPortal
            )
        {
            using (BypassPropertyChecks)
                await DeleteAsync(GroupId, dal, itemPortal);
        }

        [Delete]
        protected async Task DeleteAsync(
            string? groupId,
            [Inject] IGroupDal dal,
            [Inject] IChildDataPortal<GroupPersons> itemPortal
            )
        {
            // Delete values from persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                var criteria = new GroupCriteria(groupId);

                if (!GroupKey.HasValue)
                    await FetchAsync(criteria, dal, itemPortal);

                Persons.Clear();
                await FieldManager.UpdateChildrenAsync(this);

                await dal.DeleteAsync(criteria);
                dal.Commit(transaction);
            }
        }

        #endregion
    }
}
