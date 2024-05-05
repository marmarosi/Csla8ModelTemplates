using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Simple.Edit;

namespace Csla8RestApi.Tests.Models.Simple.Edit
{
    /// <summary>
    /// Represents an editable product object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(SimpleText), ObjectName = "Product")]
    public class Product : EditableModel<Product, ProductDto>
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

        public static readonly PropertyInfo<DateTimeOffset?> TimestampProperty = RegisterProperty<DateTimeOffset?>(nameof(Timestamp));
        public DateTimeOffset? Timestamp
        {
            get =>  GetProperty(TimestampProperty);
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
        //        typeof(Product),
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
            ProductDto dto,
            IChildDataPortalFactory childFactory
            )
        {
            DataMapper.Map(dto, this);
            await BusinessRules.CheckRulesAsync();
        }

        #endregion

        #region Factory Methods

        /// <summary>
        /// Gets a new product to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <returns>The new product.</returns>
        public static async Task<Product> NewAsync(
            IDataPortalFactory factory
            )
        {
            return await factory.GetPortal<Product>().CreateAsync();
        }

        /// <summary>
        /// Gets the specified product to edit.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="productId">The identifier of the product.</param>
        /// <returns>The requested product.</returns>
        public static async Task<Product> GetAsync(
            IDataPortalFactory factory,
            string productId
            )
        {
            return await factory.GetPortal<Product>().FetchAsync(new ProductCriteria(productId));
        }

        /// <summary>
        /// Builds a new or existing product.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        /// <param name="dto"></param>
        /// <returns>The product built.</returns>
        public static async Task<Product> BuildAsync(
            IDataPortalFactory factory,
            IChildDataPortalFactory childFactory,
            ProductDto dto
            )
        {
            long? productKey = KeyHash.Decode(ID.Product, dto.ProductId);
            Product product = productKey.HasValue ?
                await GetAsync(factory, dto.ProductId!) :
                await NewAsync(factory);
            await product.SetValuesOnBuild(dto, childFactory);
            return product;
        }

        /// <summary>
        /// Deletes the specified product.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="productId">The identifier of the product.</param>
        public static async Task DeleteAsync(
            IDataPortalFactory factory,
            string productId
            )
        {
            await factory.GetPortal<Product>().DeleteAsync(productId);
        }

        #endregion

        #region Data Access

        [Create]
        [RunLocal]
        private async Task CreateAsync()
        {
            // Load default values.
            await Task.Run(async () =>
            {
                //LoadProperty(ProductCodeProperty, "");
                await BusinessRules.CheckRulesAsync();
            });
        }

        [Fetch]
        private async Task FetchAsync(
            ProductCriteria criteria,
            [Inject] IProductDal dal
            )
        {
            // Load values from persistent storage.
            ProductDao dao = await dal.FetchAsync(criteria);
            using (BypassPropertyChecks)
                DataMapper.Map(dao, this);
        }

        [Insert]
        protected async Task InsertAsync(
            [Inject] IProductDal dal
            )
        {
            // Insert values into persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).ToNew<ProductDao>();
                    await dal.InsertAsync(dao);

                    // Set new data.
                    ProductKey = dao.ProductKey;
                    Timestamp = dao.Timestamp;
                }
                dal.Commit(transaction);
            }
        }

        [Update]
        protected async Task UpdateAsync(
            [Inject] IProductDal dal
            )
        {
            // Update values in persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                using (BypassPropertyChecks)
                {
                    var dao = Copy.PropertiesFrom(this).ToNew<ProductDao>();
                    await dal.UpdateAsync(dao);

                    // Set new data.
                    Timestamp = dao.Timestamp;
                }
                dal.Commit(transaction);
            }
        }

        [DeleteSelf]
        protected async Task DeleteSelfAsync(
            [Inject] IProductDal dal
            )
        {
            using (BypassPropertyChecks)
                await DeleteAsync(ProductId, dal);
        }

        [Delete]
        protected async Task DeleteAsync(
            string? productId,
            [Inject] IProductDal dal
            )
        {
            // Delete values from persistent storage.
            using (var transaction = dal.BeginTransaction())
            {
                await dal.DeleteAsync(new ProductCriteria(productId));
                dal.Commit(transaction);
            }
        }

        #endregion
    }
}
