using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Arrangement.Pagination
{
    /// <summary>
    /// Represents the criteria of the read-only paginated product collection.
    /// </summary>
    [Serializable]
    public class ProductListCriteria : PaginatedListCriteria
    {
        public string? ProductName { get; set; }
    }
}
