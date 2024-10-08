using Csla;
using Csla.Core;
using Csla8RestApi.Dal.Contracts;
using System.Reflection;

namespace Csla8RestApi.Models
{
    /// <summary>
    /// Wrapper for read-only model to hide server side properties.
    /// </summary>
    /// <typeparam name="T">The type of the business object.</typeparam>
    [Serializable]
    public abstract class ReadOnlyModel<T> : ReadOnlyBase<T>, IReadOnlyModel
        where T : ReadOnlyBase<T>
    {
        /// <summary>
        /// Converts the business object to data transfer object.
        /// </summary>
        /// <typeparam name="D">The class of the data transfer object.</typeparam>
        /// <returns>The data transfer object.</returns>
        public D ToDto<D>() where D : class
        {
            Type type = typeof(D);
            D dto = (D)Activator.CreateInstance(type)!;

            List<IPropertyInfo> cslaProperties = FieldManager.GetRegisteredProperties();
            List<PropertyInfo> dtoProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(fi => !fi.Name.StartsWith("__"))
                .ToList();

            foreach (var dtoProperty in dtoProperties)
            {
                var cslaProperty = cslaProperties.Find(pi => pi.Name == dtoProperty.Name);
                if (cslaProperty is not null)
                {
                    if (cslaProperty.Type.GetInterface(nameof(IReadOnlyList)) is not null)
                        SetDtoValue(dto, dtoProperty, cslaProperty, true);
                    //{
                    //    Type childType = dtoProperty.PropertyType.GenericTypeArguments[0];
                    //    IReadOnlyList cslaBase = (IReadOnlyList)GetProperty(cslaProperty)!;
                    //    if (cslaBase != null)
                    //    {
                    //        object value = cslaProperty.Type
                    //            .GetMethod("ToDto")!
                    //            .MakeGenericMethod(childType)
                    //            .Invoke(cslaBase, null)!;
                    //        dtoProperty.SetValue(dto, value);
                    //    }
                    //}
                    else if (cslaProperty.Type.GetInterface(nameof(IReadOnlyModel)) is not null)
                        SetDtoValue(dto, dtoProperty, cslaProperty, false);
                    //{
                    //    Type childType = dtoProperty.PropertyType;
                    //    IReadOnlyModel cslaBase = (IReadOnlyModel)GetProperty(cslaProperty)!;
                    //    if (cslaBase != null)
                    //    {
                    //        object value = cslaProperty.Type
                    //            .GetMethod("ToDto")!
                    //            .MakeGenericMethod(childType)
                    //            .Invoke(cslaBase, null)!;
                    //        dtoProperty.SetValue(dto, value);
                    //    }
                    //}
                    else
                        dtoProperty.SetValue(dto, GetProperty(cslaProperty));
                }
            }

            return dto;
        }

        /// <summary>
        /// Converts the business object to paginated data transfer object.
        /// </summary>
        /// <typeparam name="D">The class of the data transfer object.</typeparam>
        /// <returns>The data transfer object.</returns>
        public IPaginatedList<D> ToPaginatedDto<D>() where D : class
        {
            Type type = typeof(PaginatedList<D>);
            PaginatedList<D> dto = (PaginatedList<D>)Activator.CreateInstance(type)!;

            List<IPropertyInfo> cslaProperties = FieldManager.GetRegisteredProperties();
            List<PropertyInfo> dtoProperties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(fi => !fi.Name.StartsWith("__"))
                .ToList();

            foreach (var dtoProperty in dtoProperties)
            {
                var cslaProperty = cslaProperties.Find(pi => pi.Name == dtoProperty.Name);
                if (cslaProperty is not null)
                {
                    if (cslaProperty.Type.GetInterface(nameof(IReadOnlyList)) is not null)
                        SetDtoValue(dto, dtoProperty, cslaProperty, true);
                    //{
                    //    Type childType = dtoProperty.PropertyType.GenericTypeArguments[0];
                    //    IReadOnlyList cslaBase = (IReadOnlyList)GetProperty(cslaProperty)!;
                    //    object value = cslaProperty.Type
                    //        .GetMethod("ToDto")!
                    //        .MakeGenericMethod(childType)
                    //        .Invoke(cslaBase, null)!;
                    //    dtoProperty.SetValue(dto, value);
                    //}
                    else if (cslaProperty.Type.GetInterface(nameof(IReadOnlyModel)) is not null)
                        SetDtoValue(dto, dtoProperty, cslaProperty, false);
                    //{
                    //    Type childType = dtoProperty.PropertyType;
                    //    IReadOnlyModel cslaBase = (IReadOnlyModel)GetProperty(cslaProperty)!;
                    //    object value = cslaProperty.Type
                    //        .GetMethod("ToDto")!
                    //        .MakeGenericMethod(childType)
                    //        .Invoke(cslaBase, null)!;
                    //    dtoProperty.SetValue(dto, value);
                    //}
                    else
                        dtoProperty.SetValue(dto, GetProperty(cslaProperty));
                }
            }

            return dto;
        }

        private void SetDtoValue<D>(
            D dto,
            PropertyInfo dtoProperty,
            IPropertyInfo cslaProperty,
            bool isList
            )
        {
            Type childType = isList
                ? dtoProperty.PropertyType.GenericTypeArguments[0]
                : dtoProperty.PropertyType;
            var cslaBase = GetProperty(cslaProperty);
            if (cslaBase != null)
            {
                object value = cslaProperty.Type
                    .GetMethod("ToDto")!
                    .MakeGenericMethod(childType)
                    .Invoke(cslaBase, null)!;
                dtoProperty.SetValue(dto, value);
            }
        }
    }
}
