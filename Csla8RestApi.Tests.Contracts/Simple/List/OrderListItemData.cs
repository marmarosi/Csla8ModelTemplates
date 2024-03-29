namespace Csla8ModelTemplates.Contracts.Simple.List
{
    /// <summary>
    /// Defines the read-only order list item data.
    /// </summary>
    public class OrderListItemData
    {
        public string? OrderCode { get; set; }
        public string? OrderName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only order list item model.
    /// </summary>
    public class OrderListItemDao : OrderListItemData
    {
        public long? OrderKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only order list item model.
    /// </summary>
    public class OrderListItemDto : OrderListItemData
    {
        public string? OrderId { get; set; }
    }
}
