using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Arrangement.Pagination
{
    /// <summary>
    /// Represents the criteria of the read-only paginated team collection.
    /// </summary>
    [Serializable]
    public class PaginatedTeamListCriteria : PaginatedListCriteria
    {
        public string TeamName { get; set; }
    }
}
