using System.Text.Json.Serialization;

namespace Csla8RestApi.Tests.Contracts.Complex.Set
{
    /// <summary>
    /// Represents the criteria of the editable part object.
    /// </summary>
    [Serializable]
    public class ProductSetPartCriteria
    {
        public long? PartKey { get; set; }
        [JsonIgnore]
        public string? __productCode { get; set; } // for error messages
        [JsonIgnore]
        public string? __partCode { get; set; } // for error messages

        public ProductSetPartCriteria()
        { }

        public ProductSetPartCriteria(
            long? partKey
            )
        {
            PartKey = partKey;
        }
    }
}
