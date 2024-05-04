using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Tree.View;

namespace Csla8RestApi.Tests.Models.Tree.View
{
    /// <summary>
    /// Represents a read-only message tree.
    /// </summary>
    [Serializable]
    public class MessageTree : ReadOnlyList<MessageTree, MessageNode>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(MessageTree),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified read-only message tree.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the read-only message tree.</param>
        /// <returns>The requested read-only message tree.</returns>
        public static async Task<MessageTree> GetAsync(
            IDataPortalFactory factory,
            string? rootId
            )
        {
            return await factory.GetPortal<MessageTree>().FetchAsync(new MessageTreeCriteria(rootId));
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            MessageTreeCriteria criteria,
            [Inject] IMessageTreeDal dal,
            [Inject] IChildDataPortal<MessageNode> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<MessageNodeDao> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
