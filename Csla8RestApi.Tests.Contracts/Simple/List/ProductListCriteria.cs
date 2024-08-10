namespace Csla8RestApi.Tests.Contracts.Simple.List
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
