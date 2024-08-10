using Csla;
using Csla.Data;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts.Complex.Command;

namespace Csla8RestApi.Tests.Models.Complex.Command
{
    /// <summary>
    /// Represents an item in a read-only count products result collection.
    /// </summary>
    [Serializable]
    public class CountProductsResult : ReadOnlyModel<CountProductsResult>
    {
        #region Properties

        public static readonly PropertyInfo<int> PartCountProperty = RegisterProperty<int>(nameof(PartCount));
        public int PartCount
        {
            get => GetProperty(PartCountProperty);
            private set => LoadProperty(PartCountProperty, value);
        }

        public static readonly PropertyInfo<int> ProductCountByPartCountProperty = RegisterProperty<int>(nameof(ProductCountByPartCount));
        public int ProductCountByPartCount
        {
            get => GetProperty(ProductCountByPartCountProperty);
            private set => LoadProperty(ProductCountByPartCountProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.WriteProperty,
        //            ItemCountProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(CountProductsListItem),
        //        new IsInRole(
        //            AuthorizationActions.GetObject,
        //            "Manager"
        //            )
        //        );
        //}

        #endregion

        #region Data Access

        [FetchChild]
        private async Task FetchAsync(
            CountProductsResultDao dao
            )
        {
            // Load values from persistent storage.
            await Task.Run(() =>
            {
                DataMapper.Map(dao, this);
            });
        }

        #endregion
    }
}
