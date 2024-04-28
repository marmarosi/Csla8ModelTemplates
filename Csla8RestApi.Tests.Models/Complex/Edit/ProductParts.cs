using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Complex.Edit;

namespace Csla8RestApi.Tests.Models.Complex.Edit
{
    /// <summary>
    /// Represents an editable part collection.
    /// </summary>
    [Serializable]
    public class ProductParts : EditableList<ProductParts, ProductPart, ProductPartDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(Parts),
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
            List<ProductPartDao> list,
            [Inject] IChildDataPortal<ProductPart> itemPortal
            )
        {
            foreach (var item in list)
                Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
