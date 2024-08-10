using Csla;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Selection.ByKey;

namespace Csla8RestApi.Tests.Models.Selection.ByKey
{
    /// <summary>
    /// Represents a read-only product choice collection.
    /// </summary>
    [Serializable]
    public class ProductChoice : ReadOnlyList<ProductChoice, ChoiceItem<long?>>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductByKeyChoice),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a choice of product options that match the criteria.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria product choice.</param>
        /// <returns>The requested product choice instance.</returns>
        public static async Task<ProductChoice> GetAsync(
            IDataPortalFactory factory,
            ProductChoiceCriteria criteria
            )
        {
            return await factory.GetPortal<ProductChoice>().FetchAsync(criteria);
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            ProductChoiceCriteria criteria,
            [Inject] IProductChoiceDal dal,
            [Inject] IChildDataPortal<ChoiceItem<long?>> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ChoiceItemDao<long?>> list = await dal.FetchAsync(criteria);
                foreach (var item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        #endregion
    }
}