using Csla;
using Csla.Data;
using Csla8ModelTemplates.Contracts.Complex.Command;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Complex.Command
{
    /// <summary>
    /// Represents an item in a read-only count result collection.
    /// </summary>
    [Serializable]
    public class CountTeamsResult : ReadOnlyModel<CountTeamsResult>
    {
        #region Properties

        public static readonly PropertyInfo<int> PlayerCountProperty = RegisterProperty<int>(nameof(PlayerCount));
        public int PlayerCount
        {
            get => GetProperty(PlayerCountProperty);
            private set => LoadProperty(PlayerCountProperty, value);
        }

        public static readonly PropertyInfo<int> CountOfTeamsProperty = RegisterProperty<int>(nameof(CountOfTeams));
        public int CountOfTeams
        {
            get => GetProperty(CountOfTeamsProperty);
            private set => LoadProperty(CountOfTeamsProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.WriteProperty,
        //            ItemCountProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountTeamsListItem),
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
            CountTeamsResultDao dao
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
