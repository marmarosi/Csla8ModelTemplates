using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Complex.Edit
{
    /// <summary>
    /// Defines the editable part data.
    /// </summary>
    public abstract class ProductPartData
    {
        public string? PartCode { get; set; }
        public string? PartName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable part object.
    /// </summary>
    public class ProductPartDao : ProductPartData
    {
        public long? PartKey { get; set; }
        public long? ProductKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable part object.
    /// </summary>
    public class ProductPartDto : ProductPartData
    {
        public string? PartId { get; set; }
        public string? ProductId { get; set; }

        public ProductPartDao ToDao()
        {
            return new ProductPartDao
            {
                PartKey = KeyHash.Decode(ID.Part, PartId),
                ProductKey = KeyHash.Decode(ID.Product, ProductId),
                PartCode = PartCode,
                PartName = PartName
            };
        }
    }
}
