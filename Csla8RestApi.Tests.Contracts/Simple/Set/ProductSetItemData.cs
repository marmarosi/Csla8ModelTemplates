namespace Csla8RestApi.Tests.Contracts.Simple.Set
{
    /// <summary>
    /// Defines the editable product set item data.
    /// </summary>
    public abstract class ProductSetItemData
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable product set item object.
    /// </summary>
    public class ProductSetItemDao : ProductSetItemData
    {
        public long? ProductKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable product set item object.
    /// </summary>
    public class ProductSetItemDto : ProductSetItemData
    {
        public string? ProductId { get; set; }
    }
}
