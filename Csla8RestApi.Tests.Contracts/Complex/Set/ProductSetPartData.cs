using Csla8RestApi.Dal.Contracts;
using System.Text.Json.Serialization;

namespace Csla8RestApi.Tests.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the editable team set part data.
    /// </summary>
    public abstract class ProductSetPartData
    {
        public string? PartCode { get; set; }
        public string? PartName { get; set; }
        [JsonIgnore]
#pragma warning disable S1104
        public string? __productCode; // for error messages
#pragma warning restore S1104
    }

    /// <summary>
    /// Defines the data access object of the editable team set part object.
    /// </summary>
    public class ProductSetPartDao : ProductSetPartData
    {
        public long? PartKey { get; set; }
        public long? ProductKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable team set part object.
    /// </summary>
    public class ProductSetPartDto : ProductSetPartData
    {
        public string? PartId { get; set; }
        public string? ProductId { get; set; }

        public ProductSetPartDao ToDao()
        {
            return new ProductSetPartDao
            {
                PartKey = KeyHash.Decode(ID.Part, PartId),
                ProductKey = KeyHash.Decode(ID.Product, ProductId),
                PartCode = PartCode,
                PartName = PartName,
                __productCode = __productCode
            };
        }
    }
}
