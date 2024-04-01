namespace Csla8RestApi.Tests.Contracts.Complex.Command
{
    /// <summary>
    /// Defines the count products list item data.
    /// </summary>
    public class CountProductsResultData
    {
        public int PartCount { get; set; }
        public int ProductCountByPartCount { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the count products list item object.
    /// </summary>
    public class CountProductsResultDao : CountProductsResultData
    { }

    /// <summary>
    /// Defines the data transfer object of the count products list item object.
    /// </summary>
    public class CountProductsResultDto : CountProductsResultData
    { }
}
