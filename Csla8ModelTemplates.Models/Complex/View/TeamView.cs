using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Complex.View;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Complex.View
{
    /// <summary>
    /// Represents a read-only team object.
    /// </summary>
    [Serializable]
    public class TeamView : ReadOnlyModel<TeamView>
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

        public static readonly PropertyInfo<TeamViewPlayers> PlayersProperty = RegisterProperty<TeamViewPlayers>(nameof(Players));
        public TeamViewPlayers Players
        {
            get => GetProperty(PlayersProperty);
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
        //        typeof(TeamView),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified team details to display.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="id">The identifier of the team.</param>
        /// <returns>The requested team view.</returns>
        public static async Task<TeamView> Get(
            IDataPortalFactory factory,
            string id
            )
        {
            var criteria = new TeamViewParams(id);
            return await factory.GetPortal<TeamView>().FetchAsync(criteria.Decode());
        }

        #endregion

        #region Data Access

        [Fetch]
        private void Fetch(
            TeamViewCriteria criteria,
            [Inject] ITeamViewDal dal,
            [Inject] IChildDataPortal<TeamViewPlayers> itemsPortal
            )
        {
            // Load values from persistent storage.
            TeamViewDao dao = dal.Fetch(criteria);
            DataMapper.Map(dao, this, "Players");
            Players = itemsPortal.FetchChild(dao.Players);
        }

        #endregion
    }
}
