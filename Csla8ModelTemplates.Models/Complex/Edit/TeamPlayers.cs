using Csla;
using Csla8ModelTemplates.Contracts.Complex.Edit;
using Csla8RestApi.Models;

namespace Csla8ModelTemplates.Models.Complex.Edit
{
    /// <summary>
    /// Represents an editable player collection.
    /// </summary>
    [Serializable]
    public class TeamPlayers : EditableList<TeamPlayers, TeamPlayer, TeamPlayerDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Players),
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
            List<TeamPlayerDao> list,
            [Inject] IChildDataPortal<TeamPlayer> itemPortal
            )
        {
            foreach (var item in list)
                Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
