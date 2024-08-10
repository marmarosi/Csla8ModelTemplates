using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Complex.View;

namespace Csla8RestApi.Tests.Models.Complex.View
{
    /// <summary>
    /// Represents a read-only part collection.
    /// </summary>
    [Serializable]
    public class ProductViewParts : ReadOnlyList<ProductViewParts, ProductViewPart>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PartViews),
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
            List<ProductViewPartDao> list,
            [Inject] IChildDataPortal<ProductViewPart> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
