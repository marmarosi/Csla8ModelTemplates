using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Simple.List;

namespace Csla8RestApi.Tests.Models.Simple.List
{
    /// <summary>
    /// Represents a read-only product collection.
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
        /// Gets a list of products that matches the criteria.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the product list.</param>
        /// <returns>The requested product list.</returns>
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
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}
