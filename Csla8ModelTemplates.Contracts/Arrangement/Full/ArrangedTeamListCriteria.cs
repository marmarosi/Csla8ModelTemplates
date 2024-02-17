using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Arrangement.Full
{
    /// <summary>
    /// Represents the criteria of the read-only paginated sorted team collection.
    /// </summary>
    [Serializable]
    public class ArrangedTeamListCriteria : PaginatedSortedListCriteria
    {
        public string TeamName { get; set; }
    }
}
