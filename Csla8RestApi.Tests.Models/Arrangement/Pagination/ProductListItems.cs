using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Arrangement.Pagination;

namespace Csla8RestApi.Tests.Models.Arrangement.Pagination
{
    /// <summary>
    /// Represents a page of read-only product collection.
    /// </summary>
    [Serializable]
    public class ProductListItems : ReadOnlyList<ProductListItems, ProductListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductListItems),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private void Fetch(
            List<ProductListItemDao> list,
            [Inject] IChildDataPortal<ProductListItem> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(itemPortal.FetchChild(item));
        }

        #endregion
    }
}
