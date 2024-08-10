using Csla;
using Csla8ModelTemplates.Contracts.Complex.Command;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Complex.Command
{
    /// <summary>
    /// Represents a read-only count teams result collection.
    /// </summary>
    [Serializable]
    public class CountTeamsResults : ReadOnlyList<CountTeamsResults, CountTeamsResult>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountTeamsList),
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
            List<CountTeamsResultDao> list,
            [Inject] IChildDataPortal<CountTeamsResult> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
