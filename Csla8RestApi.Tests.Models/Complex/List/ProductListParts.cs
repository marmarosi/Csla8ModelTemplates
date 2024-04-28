using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Complex.List;

namespace Csla8RestApi.Tests.Models.Complex.List
{
    /// <summary>
    /// Represents a read-only part collection.
    /// </summary>
    [Serializable]
    public class ProductListParts : ReadOnlyList<ProductListParts, ProductListPart>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PartInfos),
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
            List<ProductListPartDao> list,
            [Inject] IChildDataPortal<ProductListPart> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
