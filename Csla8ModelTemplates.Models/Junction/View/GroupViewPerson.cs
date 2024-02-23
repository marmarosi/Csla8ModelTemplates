using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Junction.View;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Junction.View
{
    /// <summary>
    /// Represents an item in a read-only group-person collection.
    /// </summary>
    [Serializable]
    public class GroupViewPerson : ReadOnlyModel<GroupViewPerson>
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
            private set => PersonKey = KeyHash.Decode(ID.Person, value);
        }

        public static readonly PropertyInfo<string> PersonNameProperty = RegisterProperty<string>(nameof(PersonName));
        public string PersonName
        {
            get => GetProperty(PersonNameProperty);
            private set => LoadProperty(PersonNameProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            PersonNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GroupPersonView),
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
            GroupViewPersonDao dao
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
