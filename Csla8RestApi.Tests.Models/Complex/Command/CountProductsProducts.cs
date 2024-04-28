using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Complex.Command;

namespace Csla8RestApi.Tests.Models.Complex.Command
{
    /// <summary>
    /// Represents a read-only count products result collection.
    /// </summary>
    [Serializable]
    public class CountProductsResults : ReadOnlyList<CountProductsResults, CountProductsResult>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountProductsList),
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
            List<CountProductsResultDao> list,
            [Inject] IChildDataPortal<CountProductsResult> itemPortal
            )
        {
            // Load values from persistent storage.
            foreach (var item in list)
                Items.Add(await itemPortal.FetchChildAsync(item));
        }

        #endregion
    }
}
