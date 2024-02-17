using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Arrangement.Sorting
{
    /// <summary>
    /// Represents the criteria of the read-only sorted team collection.
    /// </summary>
    [Serializable]
    public class SortedTeamListCriteria : SortedListCriteria
    {
        public string TeamName { get; set; }
    }
}
