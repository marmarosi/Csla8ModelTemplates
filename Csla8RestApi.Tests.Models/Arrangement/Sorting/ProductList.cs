using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Arrangement.Sorting;

namespace Csla8RestApi.Tests.Models.Arrangement.Sorting
{
    /// <summary>
    /// Represents a read-only sorted product collection.
    /// </summary>
    [Serializable]
    public class ProductList : ReadOnlyList<ProductList, ProductListItem>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductList),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a read-only sorted product collection that matches the criteria.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the read-only product collection.</param>
        /// <returns>The requested read-only sorted product collection.</returns>
        public static async Task<ProductList> GetAsync(
            IDataPortalFactory factory,
            ProductListCriteria criteria
            )
        {
            return await factory.GetPortal<ProductList>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            ProductListCriteria criteria,
            [Inject] IProductListDal dal,
            [Inject] IChildDataPortal<ProductListItem> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ProductListItemDao> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(itemPortal.FetchChild(item));
            }
        }

        #endregion
    }
}
