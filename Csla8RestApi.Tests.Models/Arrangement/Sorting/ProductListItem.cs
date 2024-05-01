using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Arrangement.Sorting;

namespace Csla8RestApi.Tests.Models.Arrangement.Sorting
{
    /// <summary>
    /// Represents an item in a read-only product collection.
    /// </summary>
    [Serializable]
    public class ProductListItem : ReadOnlyModel<ProductListItem>
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
        //        typeof(ProductListItem),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        protected async Task FetchAsync(
            ProductListItemDao dao
            )
        {
            // Set values from data access object.
            await Task.Run(() =>
            {
                DataMapper.Map(dao, this);
            });
        }

        #endregion
    }
}
