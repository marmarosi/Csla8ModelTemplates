using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Complex.Set;

namespace Csla8RestApi.Tests.Models.Complex.Set
{
    /// <summary>
    /// Represents an editable part collection.
    /// </summary>
    [Serializable]
    public class ProductSetParts : EditableList<ProductSetParts, ProductSetPart, ProductSetPartDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductSetParts),
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
            List<ProductSetPartDao> list,
            [Inject] IChildDataPortal<ProductSetPart> childPortal
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
