using Csla;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Simple.Set;

namespace Csla8RestApi.Tests.Models.Simple.Set
{
    /// <summary>
    /// Represents an editable product collection.
    /// </summary>
    [Serializable]
    public class ProductSet : EditableList<ProductSet, ProductSetItem, ProductSetItemDto>
    {
        #region Business Rules

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductSet),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified product set to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the product set.</param>
        /// <returns>The requested product set.</returns>
        public static async Task<ProductSet> GetAsync(
            IDataPortalFactory factory,
            ProductSetCriteria criteria
            )
        {
            return await factory.GetPortal<ProductSet>().FetchAsync(criteria);
        }

        /// <summary>
        /// Rebuilds an editable product instance from the data transfer object.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="criteria">The criteria of the product set.</param>
        /// <param name="list">The data transer objects of the product set.</param>
        /// <returns>The product set built.</returns>
        public static async Task<ProductSet> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            ProductSetCriteria criteria,
            List<ProductSetItemDto> list
            )
        {
            var set = await factory.GetPortal<ProductSet>().FetchAsync(criteria);
            await set.SetValuesById(list, "TeamId", childFactory);
            return set;
        }

        #endregion

        #region Data Access

        //[Create]
        //[RunLocal]
        //private async Task CreateAsync()
        //{
        //    // Load default values.
        //    Child_Create();
        //}

        [Fetch]
        private async Task FetchAsync(
            ProductSetCriteria criteria,
            [Inject] IProductSetDal dal,
            [Inject] IChildDataPortal<ProductSetItem> itemPortal
            )
        {
            // Load values from persistent storage.
            using (LoadListMode)
            {
                List<ProductSetItemDao> list = await dal.FetchAsync(criteria);
                foreach (ProductSetItemDao item in list)
                    Add(await itemPortal.FetchChildAsync(item));
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] IProductSetDal dal
            )
        {
            // Update values in persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                await Child_UpdateAsync();
                dal.Commit(transaction);
            }
        }

        #endregion
    }
}
