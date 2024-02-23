using Csla;
using Csla8ModelTemplates.Contracts.Complex.Set;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Complex.Set
{
    /// <summary>
    /// Represents an editable player collection.
    /// </summary>
    [Serializable]
    public class TeamSetPlayers : EditableList<TeamSetPlayers, TeamSetPlayer, TeamSetPlayerDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(TeamSetPlayers),
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
            List<TeamSetPlayerDao> list,
            [Inject] IChildDataPortal<TeamSetPlayer> childPortal
            )
        {
            foreach (var item in list)
                Add(await childPortal.FetchChildAsync(item));
        }

        [UpdateChild]
        protected async Task UpdateAsync()
        {
            // Update values in persistent storage.
            await Child_UpdateAsync();
        }

        #endregion
    }
}
