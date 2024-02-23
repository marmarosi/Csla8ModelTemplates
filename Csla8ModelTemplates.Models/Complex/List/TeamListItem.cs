using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Complex.List;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Complex.List
{
    /// <summary>
    /// Represents an item in a read-only team collection.
    /// </summary>
    [Serializable]
    public class TeamListItem : ReadOnlyModel<TeamListItem>
    {
        #region Properties

        public static readonly PropertyInfo<long?> TeamKeyProperty = RegisterProperty<long?>(nameof(TeamKey));
        public long? TeamKey
        {
            get => GetProperty(TeamKeyProperty);
            private set => LoadProperty(TeamKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> TeamIdProperty = RegisterProperty<long?>(nameof(TeamId), RelationshipTypes.PrivateField);
        public string TeamId
        {
            get => KeyHash.Encode(ID.Team, TeamKey);
            private set => TeamKey = KeyHash.Decode(ID.Team, value);
        }

        public static readonly PropertyInfo<string> TeamCodeProperty = RegisterProperty<string>(nameof(TeamCode));
        public string TeamCode
        {
            get => GetProperty(TeamCodeProperty);
            private set => LoadProperty(TeamCodeProperty, value);
        }

        public static readonly PropertyInfo<string> TeamNameProperty = RegisterProperty<string>(nameof(TeamName));
        public string TeamName
        {
            get => GetProperty(TeamNameProperty);
            private set => LoadProperty(TeamNameProperty, value);
        }

        public static readonly PropertyInfo<TeamListPlayers> PlayersProperty = RegisterProperty<TeamListPlayers>(nameof(Players));
        public TeamListPlayers Players
        {
            get => ReadProperty(PlayersProperty);
            private set => LoadProperty(PlayersProperty, value);
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
        //        typeof(TeamListItem),
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
            TeamListItemDao dao,
            [Inject] IChildDataPortal<TeamListPlayers> itemPortal
            )
        {
            // Load values from persistent storage.
            DataMapper.Map(dao, this, "Players");
            Players = await itemPortal.FetchChildAsync(dao.Players);
        }

        #endregion
    }
}
