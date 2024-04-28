using Csla;
using Csla.Core;
using Csla.Data;
using Csla.Rules;
using Csla8RestApi.Dal.Contracts;
using Csla8RestApi.Models;
using Csla8RestApi.Models.Utilities;
using Csla8RestApi.Models.Validations;
using Csla8RestApi.Tests.Contracts;
using Csla8RestApi.Tests.Contracts.Complex.Set;

namespace Csla8RestApi.Tests.Models.Complex.Set
{
    /// <summary>
    /// Represents an editable part object.
    /// </summary>
    [Serializable]
    [ValidationResourceType(typeof(ComplexText), ObjectName = "Part")]
    public class ProductSetPart : EditableModel<ProductSetPart, ProductSetPartDto>
    {
        #region Properties

        public static readonly PropertyInfo<long?> PartKeyProperty = RegisterProperty<long?>(nameof(PartKey));
        public long? PartKey
        {
            get => GetProperty(PartKeyProperty);
            private set => SetProperty(PartKeyProperty, value);
        }

        public static readonly PropertyInfo<long?> PartIdProperty = RegisterProperty<long?>(nameof(PartId), RelationshipTypes.PrivateField);
        public string PartId
        {
            get => KeyHash.Encode(ID.Part, PartKey);
            set => PartKey = KeyHash.Decode(ID.Part, value);
        }

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

        public static readonly PropertyInfo<string> PartCodeProperty = RegisterProperty<string>(nameof(PartCode));
        [Required]
        [MaxLength(10)]
        public string PartCode
        {
            get => GetProperty(PartCodeProperty);
            set => SetProperty(PartCodeProperty, value);
        }

        public static readonly PropertyInfo<string> PartNameProperty = RegisterProperty<string>(nameof(PartName));
        [Required]
        [MaxLength(100)]
        public string PartName
        {
            get => GetProperty(PartNameProperty);
            set => SetProperty(PartNameProperty, value);
        }

        #endregion

        #region Business Rules

        protected override void AddBusinessRules()
        {
            // Call base class implementation to add data annotation rules to BusinessRules.
            // NOTE: DataAnnotation rules is always added with Priority = 0.
            base.AddBusinessRules();

            // Add validation rules.
            BusinessRules.AddRule(new UniquePartCodes(PartCodeProperty));

            //// Add authorization rules.
            //BusinessRules.AddRule(
            //    new IsInRole(
            //        AuthorizationActions.WriteProperty,
            //        PartCodeProperty,
            //        "Manager"
            //        )
            //    );
        }

        //private static void AddObjectAuthorizationRules()
        //{
        //    // Add authorization rules.
        //    BusinessRules.AddRule(
        //        typeof(ProductSetPart),
        //        new IsInRole(
        //            AuthorizationActions.EditObject,
        //            "Manager"
        //            )
        //        );
        //}

        private sealed class UniquePartCodes : BusinessRule
        {
            // Add additional parameters to your rule to the constructor.
            public UniquePartCodes(
                IPropertyInfo primaryProperty
                )
              : base(primaryProperty)
            {
                // If you are  going to add InputProperties make sure to
                // uncomment line below as InputProperties is NULL by default.
                //if (InputProperties == null) InputProperties = new List<IPropertyInfo>();

                // Add additional constructor code here.

                // Marke rule for IsAsync if Execute contains asyncronous code IsAsync = true; 
            }

            protected override void Execute(
                IRuleContext context
                )
            {
                ProductSetPart target = (ProductSetPart)context.Target;
                if (target.Parent == null)
                    return;

                ProductSetItem product = (ProductSetItem)target.Parent.Parent;
                var count = product.Parts.Count(part => part.PartCode == target.PartCode);
                if (count > 1)
                    context.AddErrorResult(ComplexText.Part_PartCode_NotUnique);
            }
        }

        #endregion

        #region Business Methods

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public override void SetValuesOnBuild(
            ProductSetPartDto dto,
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
            ProductSetPartDao dao
            )
        {
            // Load values from persistent storage.
            await Task.Run(() =>
            {
                using (BypassPropertyChecks)
                    DataMapper.Map(dao, this);
            });
        }

        [InsertChild]
        private async Task Child_InsertAsync(
            [Inject] IProductSetPartDal dal
            )
        {
            // Insert values into persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).ToNew<ProductSetPartDao>();
                await dal.InsertAsync(dao);

                // Set new data.
                PartKey = dao.PartKey;
            }
            //await FieldManager.UpdateChildrenAsync(this);
        }

        [UpdateChild]
        private async Task UpdateAsync(
            [Inject] IProductSetPartDal dal
            )
        {
            // Update values in persistent storage.
            using (BypassPropertyChecks)
            {
                var dao = Copy.PropertiesFrom(this).ToNew<ProductSetPartDao>();
                await dal.UpdateAsync(dao);

                // Set new data.
            }
            //await FieldManager.UpdateChildrenAsync(this);
        }

        [DeleteSelfChild]
        private async Task Child_DeleteSelfAsync(
            [Inject] IProductSetPartDal dal
            )
        {
            // Delete values from persistent storage.

            //Items.Clear();
            //await FieldManager.UpdateChildrenAsync(this);

            ProductSetPartCriteria criteria = new ProductSetPartCriteria(PartKey)
            {
                __productCode = ((ProductSetItem)Parent.Parent).ProductCode,
                __partCode = PartCode
            };
            await dal.DeleteAsync(criteria);
        }

        #endregion
    }
}
