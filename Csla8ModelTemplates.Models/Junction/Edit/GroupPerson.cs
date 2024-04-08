using Csla;
using Csla.Core;
using Csla.Data;
using Csla.Rules;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Junction.Edit;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;

namespace Csla8ModelTemplates.Models.Junction.Edit
{
    /// <summary>
    /// Represents an editable group-person object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(JunctionText), ObjectName = "GroupPerson")]
    public class GroupPerson : EditableModel<GroupPerson, GroupPersonDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PersonKeyProperty = RegisterProperty<long?>(nameof(PersonKey));
        public long? PersonKey
        {
            get => GetProperty(PersonKeyProperty);
            private set => LoadProperty(PersonKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> PersonIdProperty = RegisterProperty<long?>(nameof(PersonId), RelationshipTypes.PrivateField);
        public string PersonId
        {
            get => KeyHash.Encode(ID.Person, PersonKey);
            set => PersonKey = KeyHash.Decode(ID.Person, value);
        }

        public static readonly PropertyInfo<string> PersonNameProperty = RegisterProperty<string>(nameof(PersonName));
        public string PersonName
        {
            get => GetProperty(PersonNameProperty);
            private set => LoadProperty(PersonNameProperty, value);
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            //// Add validation rules.
            BusinessRules.AddRule(new UniquePersonIds(PersonIdProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(
            //    new IsInRole(
            //        AuthorizationActions.WriteProperty,
            //        PersonNameProperty,
            //        "Manager"
            //        )
            //    );
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPerson),
        //        new IsInRole(
        //            AuthorizationActions.EditObject,
        //            "Manager"
        //            )
        //        );
        //}

        private sealed class UniquePersonIds : BusinessRule
        {
            // Add additional parameters to your rule to the constructor.
            public UniquePersonIds(
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
                GroupPerson target = (GroupPerson)context.Target;
                if (target.Parent == null)
                    return;

                Group group = (Group)target.Parent.Parent;
                var count = group.Persons.Count(gp => gp.PersonId == target.PersonId);
                if (count > 1)
                    context.AddErrorResult(JunctionText.GroupPerson_PersonId_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public override void SetValuesOnBuild(
            GroupPersonDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this);
            BusinessRules.CheckRules();
        }

        #endregion

        #region Data Access

        [CreateChild]
        private async Task CreateAsync()
        {
            // Set values from data transfer object.
            await Task.Run(async () =>
            {
                //LoadProperty(PersonNameProperty, "");
                await BusinessRules.CheckRulesAsync();
            });
        }

        [FetchChild]
        private async Task FetchAsync(
            GroupPersonDao dao
            )
        {
            // Load values from persistent storage.
            await Task.Run(() =>
            {
                using (BypassPropertyChecks)
                    DataMapper.Map(dao, this, "GroupKey");
            });
        }

        [InsertChild]
        private async Task InsertAsync(
            Group parent,
            [Inject] IGroupPersonDal dal
            )
        {
            // Insert values into persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).ToNew<GroupPersonDao>();
                dao.GroupKey = parent.GroupKey;
                await dal.InsertAsync(dao);

                // Set new data.
                PersonKey = dao.PersonKey;
            }
            //await FieldManager.UpdateChildrenAsync(this);
        }

        //[UpdateChild]
        //private async Task UpdateAsync(
        //    Group parent,
        //    [Inject] IGroupPersonDal dal
        //    )
        //{
        //    // Update values in persistent storage.
        //    throw new NotImplementedException();
        //}

        [DeleteSelfChild]
        private async Task Child_DeleteSelfAsync(
            Group parent,
            [Inject] IGroupPersonDal dal
            )
        {
            // Delete values from persistent storage.

            //Items.Clear();
            //FieldManager.UpdateChildren(this);

            GroupPersonDao dao = Copy.PropertiesFrom(this).Omit("PersonId").ToNew<GroupPersonDao>();
            dao.GroupKey = parent.GroupKey;
            await dal.DeleteAsync(dao);
        }

        #endregion
    }
}
