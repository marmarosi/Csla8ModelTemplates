namespace Csla8RestApi.Tests.Contracts.Complex.View
{
    /// <summary>
    /// Defines the read-only part data.
    /// </summary>
    public abstract class ProductViewPartData
    {
        public string? PartCode { get; set; }
        public string? PartName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only part object.
    /// </summary>
    public class ProductViewPartDao : ProductViewPartData
    {
        public long? PartKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only part object.
    /// </summary>
    public class ProductViewPartDto : ProductViewPartData
    {
        public string? PartId { get; set; }
    }
}
