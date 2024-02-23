using Csla;
using Csla8ModelTemplates.Contracts.Complex.Command;

namespace Csla8ModelTemplates.Models.Complex.Command
{
    /// <summary>
    /// Counts the roots grouped by the number of their items.
    /// </summary>
    [Serializable]
    public class CountTeams : CommandBase<CountTeams>
    {
        #region Properties

        public static readonly PropertyInfo<string?> TeamNameProperty = RegisterProperty<string?>(nameof(TeamName));
        public string? TeamName
        {
            get => ReadProperty(TeamNameProperty);
            private set => LoadProperty(TeamNameProperty, value);
        }

        public static readonly PropertyInfo<CountTeamsResults> ResultsProperty = RegisterProperty<CountTeamsResults>(nameof(Results));
        public CountTeamsResults Results
        {
            get => ReadProperty(ResultsProperty);
            private set => LoadProperty(ResultsProperty, value);
        }

        #endregion

        #region Business Rules

        //private void Validate()
        //{
        //    if (string.IsNullOrEmpty(TeamName))
        //        throw new CommandException(ValidationText.CountTeams_TeamName_Required);
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountTeams),
        //        new IsInRole(
        //            AuthorizationActions.ExecuteMethod,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Counts the teams grouped by the number of their items.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the count teams by item count command.</param>
        /// <returns>The command instance.</returns>
        public static async Task<CountTeams> ExecuteAsync(
            IDataPortalFactory factory,
            CountTeamsCriteria criteria
            )
        {
            return await factory.GetPortal<CountTeams>().ExecuteAsync(criteria);
        }

        #endregion

        #region Data Access

        [Execute]
        private async Task ExecuteAsync(
            CountTeamsCriteria criteria,
            [Inject] ICountTeamsDal dal,
            [Inject] IChildDataPortal<CountTeamsResults> resultPortal
            )
        {
            // Execute the command.
            TeamName = criteria.TeamName;
            //Validate();
            List<CountTeamsResultDao> list = await dal.ExecuteAsync(criteria);

            // Set new data.
            Results = await resultPortal.FetchChildAsync(list);
        }
        #endregion
    }
}
