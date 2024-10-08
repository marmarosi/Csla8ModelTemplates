using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Arrangement.Pagination;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Arrangement.Pagination
{
    /// <summary>
    /// Represents an item in a read-only paginated team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamListItem : ReadOnlyModel<PaginatedTeamListItem>
    {
        #region Properties

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(nameof(TeamKey));
        public long? TeamKey
        {
            get => GetProperty(TeamKeyProperty);
            private set => LoadProperty(TeamKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> TeamIdProperty = RegisterProperty<string?>(nameof(TeamId), RelationshipTypes.PrivateField);
        public string? TeamId
        {
            get => KeyHash.Encode(ID.Team, TeamKey);
            private set => TeamKey = KeyHash.Decode(ID.Team, value);
        }

        public static readonly PropertyInfo<string?> TeamCodeProperty = RegisterProperty<string?>(nameof(TeamCode));
        public string? TeamCode
        {
            get => GetProperty(TeamCodeProperty);
            private set => LoadProperty(TeamCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> TeamNameProperty = RegisterProperty<string?>(nameof(TeamName));
        public string? TeamName
        {
            get => GetProperty(TeamNameProperty);
            private set => LoadProperty(TeamNameProperty, value);
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
        //        typeof(PaginatedTeamListItem),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        protected async Task FetchAsync(
            PaginatedTeamListItemDao dao
            )
        {
            // Set values from data access object.
            await Task.Run(() =>
            {
                DataMapper.Map(dao, this);
            });
        }

        #endregion
    }
}
