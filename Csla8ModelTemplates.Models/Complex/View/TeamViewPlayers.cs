using Csla;
using Csla8ModelTemplates.Contracts.Complex.View;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Complex.View
{
    /// <summary>
    /// Represents a read-only player collection.
    /// </summary>
    [Serializable]
    public class TeamViewPlayers : ReadOnlyList<TeamViewPlayers, TeamViewPlayer>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PlayerViews),
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
            List<TeamViewPlayerDao> list,
            [Inject] IChildDataPortal<TeamViewPlayer> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
