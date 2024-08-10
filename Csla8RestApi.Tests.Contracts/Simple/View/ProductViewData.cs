namespace Csla8RestApi.Tests.Contracts.Simple.View
{
    /// <summary>
    /// Defines the read-only product data.
    /// </summary>
    public abstract class ProductViewData
    {
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only product model.
    /// </summary>
    public class ProductViewDao : ProductViewData
    {
        public long? ProductKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only product model.
    /// </summary>
    public class ProductViewDto : ProductViewData
    {
        public string? ProductId { get; set; }
    }
}
