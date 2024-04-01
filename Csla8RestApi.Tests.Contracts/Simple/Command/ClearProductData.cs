namespace Csla8RestApi.Tests.Contracts.Simple.Command
{
    /// <summary>
    /// Defines the data access object of the clear product command.
    /// </summary>
    public class ClearProductData
    {
        public string? ProductName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the clear product command.
    /// </summary>
    public class ClearProductDao : ClearProductData
    {
        public long? ProductKey { get; set; }

        public ClearProductDao(
            long? productKey,
            string? productName
            )
        {
            ProductKey = productKey;
            ProductName = productName;
        }
    }

    /// <summary>
    /// Defines the data transfer object of the clear product command.
    /// </summary>
    [Serializable]
    public class ClearProductDto : ClearProductData
    {
        public string? ProductId { get; set; }
    }
}
