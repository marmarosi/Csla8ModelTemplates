using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Arrangement.Sorting
{
    /// <summary>
    /// Represents the criteria of the read-only sorted product collection.
    /// </summary>
    [Serializable]
    public class ProductListCriteria : SortedListCriteria
    {
        public string? ProductName { get; set; }
    }
}
