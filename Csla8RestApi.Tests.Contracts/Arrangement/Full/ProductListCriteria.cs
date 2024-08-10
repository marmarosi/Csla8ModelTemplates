using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Arrangement.Full
{
    /// <summary>
    /// Represents the criteria of the read-only paginated sorted product collection.
    /// </summary>
    [Serializable]
    public class ProductListCriteria : PaginatedSortedListCriteria
    {
        public string? ProductName { get; set; }
    }
}
