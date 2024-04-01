namespace Csla8RestApi.Tests.Contracts.Simple.List
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
    /// Defines the data access object of the read-only product list item model.
    /// </summary>
    public class ProductListItemDao : ProductListItemData
    {
        public long? ProductKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only product list item model.
    /// </summary>
    public class ProductListItemDto : ProductListItemData
    {
        public string? ProductId { get; set; }
    }
}
