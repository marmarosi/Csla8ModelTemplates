namespace Csla8RestApi.Tests.Contracts.Complex.List
{
    /// <summary>
    /// Defines the read-only product list item data.
    /// </summary>
    public class ProductListItemData
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only product list item object.
    /// </summary>
    public class ProductListItemDao : ProductListItemData
    {
        public long? ProductKey { get; set; }
        public required List<ProductListPartDao> Parts { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only product list item object.
    /// </summary>
    public class ProductListItemDto : ProductListItemData
    {
        public string? ProductId { get; set; }
        public required List<ProductListPartDto> Parts { get; set; }
    }
}
