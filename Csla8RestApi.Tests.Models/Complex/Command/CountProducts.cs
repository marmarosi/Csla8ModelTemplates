using Csla;
using Csla8RestApi.Tests.Contracts.Complex.Command;

namespace Csla8RestApi.Tests.Models.Complex.Command
{
    /// <summary>
    /// Represents the count products command.
    /// </summary>
    [Serializable]
    public class CountProducts : CommandBase<CountProducts>
    {
        #region Properties

        public static readonly PropertyInfo<string?> ProductNameProperty = RegisterProperty<string?>(nameof(ProductName));
        public string? ProductName
        {
            get => ReadProperty(ProductNameProperty);
            private set => LoadProperty(ProductNameProperty, value);
        }

        public static readonly PropertyInfo<CountProductsResults> ResultsProperty = RegisterProperty<CountProductsResults>(nameof(Results));
        public CountProductsResults Results
        {
            get => ReadProperty(ResultsProperty);
            private set => LoadProperty(ResultsProperty, value);
        }

        #endregion

        #region Business Rules

        //private void Validate()
        //{
        //    if (string.IsNullOrEmpty(CountProductsName))
        //        throw new CommandException(ValidationText.CountProducts_CountProductsName_Required);
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountProducts),
        //        new IsInRole(
        //            AuthorizationActions.ExecuteMethod,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Factory Methods

        /// <summary>
        /// Executes the count products command.
        /// </summary>
        /// <param name="factory">The data portal factory.</param>
        /// <param name="criteria">The criteria of the count products command.</param>
        /// <returns>The command instance.</returns>
        public static async Task<CountProducts> ExecuteAsync(
            IDataPortalFactory factory,
            CountProductsCriteria criteria
            )
        {
            return await factory.GetPortal<CountProducts>().ExecuteAsync(criteria);
        }

        #endregion

        #region Data Access

        [Execute]
        private async Task ExecuteAsync(
            CountProductsCriteria criteria,
            [Inject] ICountProductsDal dal,
            [Inject] IChildDataPortal<CountProductsResults> resultPortal
            )
        {
            // Execute the command.
            ProductName = criteria.ProductName;
            //Validate();
            List<CountProductsResultDao> list = await dal.ExecuteAsync(criteria);

            // Set new data.
            Results = await resultPortal.FetchChildAsync(list);
        }
        #endregion
    }
}
