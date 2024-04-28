using Csla;
using Csla.Data;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Complex.List;

namespace Csla8RestApi.Tests.Models.Complex.List
{
    /// <summary>
    /// Represents an item in a read-only part collection.
    /// </summary>
    [Serializable]
    public class ProductListPart : ReadOnlyModel<ProductListPart>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PartKeyProperty = RegisterProperty<long?>(nameof(PartKey));
        public long? PartKey
        {
            get => GetProperty(PartKeyProperty);
            private set => LoadProperty(PartKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> PartIdProperty = RegisterProperty<long?>(nameof(PartId), RelationshipTypes.PrivateField);
        public string PartId
        {
            get => KeyHash.Encode(ID.Part, PartKey);
            private set => PartKey = KeyHash.Decode(ID.Part, value);
        }

        public static readonly PropertyInfo<string> PartCodeProperty = RegisterProperty<string>(nameof(PartCode));
        public string PartCode
        {
            get => GetProperty(PartCodeProperty);
            private set => LoadProperty(PartCodeProperty, value);
        }

        public static readonly PropertyInfo<string> PartNameProperty = RegisterProperty<string>(nameof(PartName));
        public string PartName
        {
            get => GetProperty(PartNameProperty);
            private set => LoadProperty(PartNameProperty, value);
        }

        #endregion

        #region Business Rules

        //protected override void AddBusinessRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        new IsInRole(
        //            AuthorizationActions.ReadProperty,
        //            PartNameProperty,
        //            "Manager"
        //            )
        //        );
        //}

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(PartInfo),
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
            ProductListPartDao dao
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
