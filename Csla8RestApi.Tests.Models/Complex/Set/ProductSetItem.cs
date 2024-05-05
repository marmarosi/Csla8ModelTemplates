using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Complex.Set;

namespace Csla8RestApi.Tests.Models.Complex.Set
{
    /// <summary>
    /// Represents an editable product object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ComplexText), ObjectName = "ProductSetItem")]
    public class ProductSetItem : EditableModel<ProductSetItem, ProductSetItemDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> ProductKeyProperty = RegisterProperty<long?>(nameof(ProductKey));
        public long? ProductKey
        {
            get => GetProperty(ProductKeyProperty);
            private set => SetProperty(ProductKeyProperty, value);
        }

        public static readonly PropertyInfo<string?> ProductIdProperty = RegisterProperty<string?>(nameof(ProductId), RelationshipTypes.PrivateField);
        public string? ProductId
        {
            get => KeyHash.Encode(ID.Product, ProductKey);
            set => ProductKey = KeyHash.Decode(ID.Product, value);
        }

        public static readonly PropertyInfo<string?> ProductCodeProperty = RegisterProperty<string?>(nameof(ProductCode));
        [Required]
        [MaxLength(10)]
        public string? ProductCode
        {
            get => GetProperty(ProductCodeProperty);
            set => SetProperty(ProductCodeProperty, value);
        }

        public static readonly PropertyInfo<string?> ProductNameProperty = RegisterProperty<string?>(nameof(ProductName));
        [Required]
        [MaxLength(100)]
        public string? ProductName
        {
            get => GetProperty(ProductNameProperty);
            set => SetProperty(ProductNameProperty, value);
        }

        public static readonly PropertyInfo<ProductSetParts> PartsProperty = RegisterProperty<ProductSetParts>(nameof(Parts));
        public ProductSetParts Parts
        {
            get => GetProperty(PartsProperty);
            private set => LoadProperty(PartsProperty, value);
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
        public override async Task SetValuesOnBuild(
            ProductSetItemDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this, "Parts");
            await BusinessRules.CheckRulesAsync();
            await Parts.SetValuesById(dto.Parts, "PartId", childFactory);
        }

        #endregion

        #region Data Access

        [CreateChild]
        private async Task CreateAsync(
            [Inject] IChildDataPortal<ProductSetParts> itemsPortal
            )
        {
            // Set values from data transfer object.
            Parts = await itemsPortal.CreateChildAsync();
            //LoadProperty(ProductCodeProperty, "");
            await BusinessRules.CheckRulesAsync();
        }

        [FetchChild]
        private async Task FetchAsync(
            ProductSetItemDao dao,
            [Inject] IChildDataPortal<ProductSetParts> itemsPortal
            )
        {
            // Set values from data access object.
            using (BypassPropertyChecks)
            {
                DataMapper.Map(dao, this, "Parts");
                Parts = await itemsPortal.FetchChildAsync(dao.Parts);
            }
        }

        [InsertChild]
        private async Task InsertAsync(
            [Inject] IProductSetItemDal dal
            )
        {
            // Insert values into persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).Omit("Parts").ToNew<ProductSetItemDao>();
                await dal.InsertAsync(dao);

                // Set new data.
                ProductKey = dao.ProductKey;
                Timestamp = dao.Timestamp;
            }
            await FieldManager.UpdateChildrenAsync(this);
        }

        [UpdateChild]
        private async Task UpdateAsync(
            [Inject] IProductSetItemDal dal
            )
        {
            // Update values in persistent storage.
            if (IsSelfDirty)
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).Omit("Parts").ToNew<ProductSetItemDao>();
                    await dal.UpdateAsync(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
            }
            await FieldManager.UpdateChildrenAsync(this);
        }

        [DeleteSelfChild]
        private async Task DeleteSelfAsync(
            [Inject] IProductSetItemDal dal
            )
        {
            // Delete values from persistent storage.
            if (ProductKey.HasValue)
            {
                Parts.Clear();
                await FieldManager.UpdateChildrenAsync(this);

                var criteria = new ProductSetItemCriteria(ProductKey);
                await dal.DeleteAsync(criteria);
            }
        }

        #endregion
    }
}
