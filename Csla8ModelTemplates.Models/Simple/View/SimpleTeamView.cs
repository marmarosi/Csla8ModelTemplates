using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts;
using Csla8ModelTemplates.Contracts.Simple.View;
using Csla8RestApi.Models;
using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Models.Simple.View
{
    /// <summary>
    /// Represents a read-only team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamView : ReadOnlyModel<SimpleTeamView>
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
        //        typeof(SimpleTeamView),
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
        public static async Task<SimpleTeamView> GetAsync(
            IDataPortalFactory factory,
            string id
            )
        {
            var criteria = new SimpleTeamViewParams(id);
            return await factory.GetPortal<SimpleTeamView>().FetchAsync(criteria.Decode());
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            SimpleTeamViewCriteria criteria,
            [Inject] ISimpleTeamViewDal dal
            )
        {
            // Set values from data access object.
            SimpleTeamViewDao dao = await dal.FetchAsync(criteria);
            DataMapper.Map(dao, this);
        }

        #endregion
    }
}
