namespace Csla8RestApi.Tests.Contracts.Complex.List
{
    /// <summary>
    /// Defines the read-only part list item data.
    /// </summary>
    public abstract class ProductListPartData
    {
        public string? PartCode { get; set; }
        public string? PartName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only part object.
    /// </summary>
    public class ProductListPartDao : ProductListPartData
    {
        public long? PartKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only part object.
    /// </summary>
    public class ProductListPartDto : ProductListPartData
    {
        public string? PartId { get; set; }
    }
}
