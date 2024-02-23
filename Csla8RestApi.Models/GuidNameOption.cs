using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Represents a code-name option in a read-only choice object.
    /// </summary>
    [Serializable]
    public class GuidNameOption : ReadOnlyModel<GuidNameOption>
    {
        #region Properties

        public static readonly PropertyInfo<Guid?> GuidProperty = RegisterProperty<Guid?>(nameof(Guid));
        public Guid? Guid
        {
            get => GetProperty(GuidProperty);
            private set => LoadProperty(GuidProperty, value);
        }

        public static readonly PropertyInfo<string> NameProperty = RegisterProperty<string>(nameof(Name));
        public string Name
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
        //            TeamNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(GuidNameOption),
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
            GuidNameOptionDao dao
            )
        {
            // Set values from data access object.
            DataMapper.Map(dao, this);
        }

        #endregion
    }
}
