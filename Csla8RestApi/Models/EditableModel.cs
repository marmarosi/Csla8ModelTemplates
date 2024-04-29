using Csla;
using Csla.Core;
using Csla.Rules;
using Csla8RestApi.Models.Validations;
using System.Collections;
using System.Reflection;
using System.Text.Json.Serialization;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Wrapper for editable models to hide server side properties.
    /// </summary>
    /// <remarks>
    /// JsonIgnore is not applied to hidden inherited properties.
    /// </remarks>
    /// <typeparam name="T">The type of the business object.</typeparam>
    /// <typeparam name="Dto">The type of the data access object.</typeparam>
    [Serializable]
    public abstract class EditableModel<T, Dto> : BusinessBase<T>, IEditableModel<Dto>
        where T : BusinessBase<T>
        where Dto : class
    {
        #region Properties

        [JsonIgnore]
        public new virtual IParent Parent => base.Parent;

        [JsonIgnore]
        public new virtual bool IsNew => base.IsNew;

        [JsonIgnore]
        public new virtual bool IsDeleted => base.IsDeleted;

        [JsonIgnore]
        public override bool IsDirty => base.IsDirty;

        [JsonIgnore]
        public override bool IsSelfDirty => base.IsSelfDirty;

        [JsonIgnore]
        public override bool IsSavable => base.IsSavable;

        [JsonIgnore]
        public new virtual bool IsChild => base.IsChild;

        [JsonIgnore]
        public override bool IsSelfValid => base.IsSelfValid;

        [JsonIgnore]
        public override bool IsBusy => base.IsBusy;

        [JsonIgnore]
        public override bool IsSelfBusy => base.IsSelfBusy;

        [JsonIgnore]
        public override BrokenRulesCollection BrokenRulesCollection => base.BrokenRulesCollection;

        #endregion

        #region IsValid

        [JsonIgnore]
        public override bool IsValid
        {
            get
            {
                if (Parent == null)
                {
                    if (base.IsValid)
                        return true;
                    else
                        return HandleValidationError();
                }
                else
                    return base.IsValid;
            }
        }

        private bool HandleValidationError()
        {
            var messages = new List<ValidationMessage>();
            CollectMessages(this, "", ref messages);
            throw new BrokenRulesException(messages);
        }

        #endregion

        #region CollectMessages

        /// <summary>
        /// Gathers and formats broken rule messages.
        /// </summary>
        /// <param name="model">The actual business object to collect the broken rule messsages from.</param>
        /// <param name="prefix">The prefix to add property descriptions on the actual business object.</param>
        /// <param name="messages">The collection point of the formatted messages.</param>
        public void CollectMessages(
            BusinessBase model,
            string prefix,
            ref List<ValidationMessage> messages
            )
        {
            foreach (var brokenRule in BrokenRulesCollection)
                messages.Add(new ValidationMessage(GetType().Name, prefix, brokenRule));

            // Check child objects and collections.
            List<IPropertyInfo> propertyInfos = FieldManager.GetRegisteredProperties().ToList();
            foreach (var propertyInfo in propertyInfos)
            {
                if (propertyInfo.Type.GetInterface(nameof(IBusinessBase)) is not null)
                {
                    IEditableModel<Dto> child = (IEditableModel<Dto>)GetProperty(propertyInfo);
                    child.CollectMessages(
                        (BusinessBase)child,
                        prefix + propertyInfo.Name + ".",
                        ref messages
                        );
                }
                else if (propertyInfo.Type.GetInterface(nameof(IEditableCollection)) is not null)
                {
                    var property = GetProperty(propertyInfo);
                    var collection = (IList)property;
                    for (int i = 0; i < collection.Count; i++)
                    {
                        IEditableModel<Dto> child = (IEditableModel<Dto>)collection[i]!;
                        child.CollectMessages(
                            (BusinessBase)child,
                            prefix + propertyInfo.Name + "[" + i + "].",
                            ref messages
                            );
                    }
                }
            }
        }

        #endregion

        #region SetValuesOnBuild

        /// <summary>
        /// Updates an editable model and its children from the data transfer object.
        /// </summary>
        /// <param name="dto">The data transfer object.</param>
        /// <param name="childFactory">The child data portal factory.</param>
        public virtual void SetValuesOnBuild(
            Dto dto,
            IChildDataPortalFactory childFactory
            )
        {
            throw new NotImplementedException();
        }

        #endregion

        #region ToDto

        /// <summary>
        /// Converts the business object to data transfer object.
        /// </summary>
        /// <returns>The data transfer object.</returns>
        public Dto ToDto()
        {
            Type type = typeof(Dto);
            Dto dto = (Dto)Activator.CreateInstance(type)!;

            List<IPropertyInfo> cslaProperties = FieldManager.GetRegisteredProperties();
            List<PropertyInfo> dtoProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(fi => !fi.Name.StartsWith("__"))
                .ToList();

            foreach (var dtoProperty in dtoProperties)
            {
                var cslaProperty = cslaProperties.Find(pi => pi.Name == dtoProperty.Name);
                if (cslaProperty is not null)
                {
                    if (cslaProperty.Type.GetInterface(nameof(IEditableList<Dto, T>) + "`2") is not null)
                        SetDtoValue(dto, dtoProperty, cslaProperty);
                    else if (cslaProperty.Type.GetInterface(nameof(IEditableModel<Dto>) + "`1") is not null)
                        SetDtoValue(dto, dtoProperty, cslaProperty);
                    else
                        dtoProperty.SetValue(dto, GetProperty(cslaProperty));
                }
            }

            return dto;
        }

        private void SetDtoValue(
            Dto dto,
            PropertyInfo dtoProperty,
            IPropertyInfo cslaProperty
            )
        {
            var cslaBase = GetProperty(cslaProperty);
            if (cslaBase != null)
            {
                object value = cslaProperty.Type
                    .GetMethod("ToDto")!
                    .Invoke(cslaBase, null)!;
                dtoProperty.SetValue(dto, value);
            }
        }

        #endregion
    }
}
