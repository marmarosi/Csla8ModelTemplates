using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Represents an item in a read-only choice object.
    /// </summary>
    [Serializable]
    public class ChoiceItem<T> : ReadOnlyModel<ChoiceItem<T>>
    {
        #region Properties

        public static readonly PropertyInfo<T> ValueProperty = RegisterProperty<T>(nameof(Value));
        public T Value
        {
            get => GetProperty(ValueProperty);
            private set => LoadProperty(ValueProperty, value);
        }

        public static readonly PropertyInfo<string?> NameProperty = RegisterProperty<string?>(nameof(Name));
        public string? Name
        {
            get => GetProperty(NameProperty);
            private set => LoadProperty(NameProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            NameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ChoiceItem<T>),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private void Fetch(
            ChoiceItemDao<T> dao
            )
        {
            // Set values from data access object.
            DataMapper.Map(dao, this);
        }

        #endregion
    }
}
