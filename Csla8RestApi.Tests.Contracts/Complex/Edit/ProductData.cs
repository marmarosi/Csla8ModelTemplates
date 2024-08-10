namespace Csla8RestApi.Tests.Contracts.Complex.Edit
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
        public List<ProductPartDao> Parts { get; set; }

        public ProductDao()
        {
            Parts = new List<ProductPartDao>();
        }
    }

    /// <summary>
    /// Defines the data transfer object of the editable product object.
    /// </summary>
    public class ProductDto : ProductData
    {
        public string? ProductId { get; set; }
        public List<ProductPartDto> Parts { get; set; }

        public ProductDto()
        {
            Parts = new List<ProductPartDto>();
        }
    }
}
