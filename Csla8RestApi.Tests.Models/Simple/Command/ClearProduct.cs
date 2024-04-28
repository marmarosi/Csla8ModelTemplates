using Csla;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Simple.Command;

namespace Csla8RestApi.Tests.Models.Simple.Command
{
    /// <summary>
    /// Represents the rename clear product command.
    /// </summary>
    [Serializable]
    public class ClearProduct : CommandBase<ClearProduct>
    {
        #region Properties

        public static readonly PropertyInfo<long?> ProductKeyProperty = RegisterProperty<long?>(nameof(ProductKey));
        public long? ProductKey
        {
            get => ReadProperty(ProductKeyProperty);
            private set => LoadProperty(ProductKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> ProductIdProperty = RegisterProperty<long?>(nameof(ProductId), RelationshipTypes.PrivateField);
        public string ProductId
        {
            get => KeyHash.Encode(ID.Product, ProductKey);
            set => ProductKey = KeyHash.Decode(ID.Product, value);
        }

        public static readonly PropertyInfo<string?> ProductNameProperty = RegisterProperty<string?>(c => c.ProductName);
        public string? ProductName
        {
            get => ReadProperty(ProductNameProperty);
            private set => LoadProperty(ProductNameProperty, value);
        }

        public static readonly PropertyInfo<bool> ResultProperty = RegisterProperty<bool>(c => c.Result);
        public bool Result
        {
            get => ReadProperty(ResultProperty);
            private set => LoadProperty(ResultProperty, value);
        }

        #endregion

        #region Business Rules

        private void Validate()
        {
            if (string.IsNullOrEmpty(ProductName))
                throw new BrokenRulesException(
                    nameof(ClearProduct),
                    nameof(ProductName),
                    SimpleText.ClearProduct_ProductName_Required
                    );
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ClearProduct),
        //        new IsInRole(
        //            AuthorizationActions.ExecuteMethod,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Executes the rename clear product command.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="dto">The data transer object of the rename clear product command.</param>
        /// <returns>The command instance.</returns>
        public static async Task<ClearProduct> ExecuteAsync(
            IDataPortalFactory factory,
            ClearProductDto dto
            )
        {
            return await factory.GetPortal<ClearProduct>().ExecuteAsync(dto);
        }

        #endregion

        #region Data Access

        [Execute]
        private async Task ExecuteAsync(
            ClearProductDto dto,
            [Inject] IClearProductDal dal
            )
        {
            // Execute the command.
            ProductId = dto.ProductId!;
            ProductName = dto.ProductName;
            Validate();

            using (var transaction = dal.BeginTransaction())
            {
                ClearProductDao dao = new ClearProductDao(ProductKey ?? 0, ProductName);
                await dal.ExecuteAsync(dao);
            }

            // Set new data.
            Result = true;
        }

        #endregion
    }
}
