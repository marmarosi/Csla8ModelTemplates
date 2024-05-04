using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Simple.View;

namespace Csla8RestApi.Tests.Models.Simple.View
{
    /// <summary>
    /// Represents a read-only product object.
    /// </summary>
    [Serializable]
    public class ProductView : ReadOnlyModel<ProductView>
    {
        #region Properties

        public static readonly PropertyInfo<long?> ProductKeyProperty = RegisterProperty<long?>(nameof(ProductKey));
        public long? ProductKey
        {
            get => GetProperty(ProductKeyProperty);
            private set => LoadProperty(ProductKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> ProductIdProperty = RegisterProperty<string?>(nameof(ProductId), RelationshipTypes.PrivateField);
        public string? ProductId
        {
            get => KeyHash.Encode(ID.Product, ProductKey);
            private set => ProductKey = KeyHash.Decode(ID.Product, value);
        }

        public static readonly PropertyInfo<string?> ProductCodeProperty = RegisterProperty<string?>(nameof(ProductCode));
        public string? ProductCode
        {
            get => GetProperty(ProductCodeProperty);
            private set => LoadProperty(ProductCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> ProductNameProperty = RegisterProperty<string?>(nameof(ProductName));
        public string? ProductName
        {
            get => GetProperty(ProductNameProperty);
            private set => LoadProperty(ProductNameProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            ProductNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductView),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets the specified product details to display.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="id">The identifier of the product.</param>
        /// <returns>The requested product view.</returns>
        public static async Task<ProductView> GetAsync(
            IDataPortalFactory factory,
            string id
            )
        {
            return await factory.GetPortal<ProductView>().FetchAsync(new ProductViewCriteria(id));
        }

        #endregion

        #region Data Access

        [Fetch]
        private async Task FetchAsync(
            ProductViewCriteria criteria,
            [Inject] IProductViewDal dal
            )
        {
            // Set values from data access object.
            ProductViewDao dao = await dal.FetchAsync(criteria);
            DataMapper.Map(dao, this);
        }

        #endregion
    }
}
