using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Simple.Set;

namespace Csla8RestApi.Tests.Models.Simple.Set
{
    /// <summary>
    /// Represents an editable product object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(SimpleText), ObjectName = "ProductSetItem")]
    public class ProductSetItem : EditableModel<ProductSetItem, ProductSetItemDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> ProductKeyProperty = RegisterProperty<long?>(nameof(ProductKey));
        public long? ProductKey
        {
            get => GetProperty(ProductKeyProperty);
            private set => SetProperty(ProductKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> ProductIdProperty = RegisterProperty<long?>(nameof(ProductId), RelationshipTypes.PrivateField);
        public string ProductId
        {
            get => KeyHash.Encode(ID.Product, ProductKey);
            set => ProductKey = KeyHash.Decode(ID.Product, value);
        }

        public static readonly PropertyInfo<string> ProductCodeProperty = RegisterProperty<string>(nameof(ProductCode));
        [Required]
        [MaxLength(10)]
        public string ProductCode
        {
            get => GetProperty(ProductCodeProperty);
            set => SetProperty(ProductCodeProperty, value);
        }

        public static readonly PropertyInfo<string> ProductNameProperty = RegisterProperty<string>(nameof(ProductName));
        [Required]
        [MaxLength(100)]
        public string ProductName
        {
            get => GetProperty(ProductNameProperty);
            set => SetProperty(ProductNameProperty, value);
        }

        public static readonly PropertyInfo<DateTimeOffset?> TimestampProperty = RegisterProperty<DateTimeOffset?>(nameof(Timestamp));
        public DateTimeOffset? Timestamp
        {
            get => GetProperty(TimestampProperty);
            private set => LoadProperty(TimestampProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Call base class implementation to add data annotation rules to BusinessRules.
        //    // NOTE: DataAnnotation rules is always added with Priority = 0.
        //    base.AddBusinessRules();

        //    // Add validation rules.
        //    BusinessRules.AddRule(new Required(ProductNameProperty));

        //    // Add authorization rules.
        //    BusinessRules.AddRule(new IsInRole(
        //        AuthorizationActions.ReadProperty,
        //        ProductNameProperty,
        //        "Manager"
        //        ));
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductSetItem),
        //        new IsInRole(
        //            AuthorizationActions.EditObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public override void SetValuesOnBuild(
            ProductSetItemDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this);
            BusinessRules.CheckRules();
        }

        #endregion

        #region Data Access

        [CreateChild]
        private async Task CreateAsync()
        {
            // Set values from data transfer object.
            //LoadProperty(ProductCodeProperty, "");
            await BusinessRules.CheckRulesAsync();
        }

        [FetchChild]
        private async Task FetchAsync(
            ProductSetItemDao dao
            )
        {
            // Set values from data access object.
            await Task.Run(() =>
            {
                using (BypassPropertyChecks)
                    DataMapper.Map(dao, this);
            });
        }

        [InsertChild]
        private async Task InsertAsync(
            [Inject] IProductSetItemDal dal
            )
        {
            // Insert values into persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).ToNew<ProductSetItemDao>();
                await dal.InsertAsync(dao);

                // Set new data.
                ProductKey = dao.ProductKey;
                Timestamp = dao.Timestamp;
            }
        }

        [UpdateChild]
        private async Task UpdateAsync(
            [Inject] IProductSetItemDal dal
            )
        {
            // Update values in persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).ToNew<ProductSetItemDao>();
                await dal.UpdateAsync(dao);

                // Set new data.
                Timestamp = dao.Timestamp;
            }
        }

        [DeleteSelfChild]
        private async Task DeleteSelfAsync(
            [Inject] IProductSetItemDal dal
            )
        {
            // Delete values from persistent storage.
            if (ProductKey.HasValue)
            {
                var criteria = new ProductSetItemCriteria(ProductKey);
                await dal.DeleteAsync(criteria);
            }
        }

        #endregion
    }
}
