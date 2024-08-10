using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Tree.View;

namespace Csla8RestApi.Tests.Models.Tree.View
{
    /// <summary>
    /// Represents a read-only message node collection.
    /// </summary>
    [Serializable]
    public class MessageNodes : ReadOnlyList<MessageNodes, MessageNode>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(MessageNodeList),
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
            List<MessageNodeDao> list,
            [Inject] IChildDataPortal<MessageNode> childPortal
            )
        {
            using (LoadListMode)
            {
                foreach (var item in list)
                    Add(await childPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
