namespace Csla8RestApi.Tests.Contracts.Complex.List
{
    /// <summary>
    /// Represents the criteria of the read-only product collection.
    /// </summary>
    [Serializable]
    public class ProductListCriteria
    {
        public string? ProductName { get; set; }
    }
}
