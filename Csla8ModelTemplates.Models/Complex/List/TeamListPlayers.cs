using Csla;
using Csla8ModelTemplates.Contracts.Complex.List;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Complex.List
{
    /// <summary>
    /// Represents a read-only player collection.
    /// </summary>
    [Serializable]
    public class TeamListPlayers : ReadOnlyList<TeamListPlayers, TeamListPlayer>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PlayerInfos),
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
            List<TeamListPlayerDao> list,
            [Inject] IChildDataPortal<TeamListPlayer> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
