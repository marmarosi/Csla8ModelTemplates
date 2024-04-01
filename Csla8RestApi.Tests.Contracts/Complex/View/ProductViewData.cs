namespace Csla8RestApi.Tests.Contracts.Complex.View
{
    /// <summary>
    /// Defines the read-only product data.
    /// </summary>
    public class ProductViewData
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only product object.
    /// </summary>
    public class ProductViewDao : ProductViewData
    {
        public long? ProductKey { get; set; }
        public required List<ProductViewPartDao> Parts { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only product object.
    /// </summary>
    public class ProductViewDto : ProductViewData
    {
        public string? ProductId { get; set; }
        public required List<ProductViewPartDto> Parts { get; set; }
    }
}
