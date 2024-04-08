namespace Csla8RestApi.Tests.Contracts.Simple.Edit
{
    /// <summary>
    /// Defines the editable product data.
    /// </summary>
    public abstract class ProductData
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable product object.
    /// </summary>
    public class ProductDao : ProductData
    {
        public long? ProductKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable product object.
    /// </summary>
    public class ProductDto : ProductData
    {
        public string? ProductId { get; set; }
    }
}
