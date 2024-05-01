using Csla;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Arrangement.Full;

namespace Csla8RestApi.Tests.Models.Arrangement.Full
{
    /// <summary>
    /// Represents a read-only paginated sorted product collection.
    /// </summary>
    [Serializable]
    public class ProductList : ReadOnlyModel<ProductList>
    {
        #region Properties

        public static readonly PropertyInfo<ProductListItems?> DataProperty = RegisterProperty<ProductListItems?>(nameof(Data));
        public ProductListItems? Data
        {
            get => GetProperty(DataProperty);
            private set => LoadProperty(DataProperty, value);
        }

        public static readonly PropertyInfo<int> TotalCountProperty = RegisterProperty<int>(c => c.TotalCount);
        public int TotalCount
        {
            get => GetProperty(TotalCountProperty);
            private set => LoadProperty(TotalCountProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            DataProperty,
        //            "Manager"
        //            )
        //        );
        //}

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
        /// Gets the specified read-only paginated sorted product collection.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the read-only product.</param>
        /// <returns>The requested read-only product list.</returns>
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
            [Inject] IChildDataPortal<ProductListItems> itemsPortal
            )
        {
            // Load values from persistent storage.
            IPaginatedList<ProductListItemDao> dao = await dal.FetchAsync(criteria);
            Data = await itemsPortal.FetchChildAsync(dao.Data);
            TotalCount = dao.TotalCount;
        }

        #endregion
    }
}
