namespace Csla8RestApi.Tests.Contracts.Complex.Command
{
    /// <summary>
    /// Represents the criteria of the count products by part count command.
    /// </summary>
    [Serializable]
    public class CountProductsCriteria
    {
        public string? ProductName { get; set; }

        public CountProductsCriteria()
        { }

        public CountProductsCriteria(
            string productName
            )
        {
            ProductName = productName;
        }
    }
}
