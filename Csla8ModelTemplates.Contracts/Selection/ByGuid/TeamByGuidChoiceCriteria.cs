using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.ByGuid
{
    /// <summary>
    /// Represents the criteria of the read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamByGuidChoiceCriteria : ChoiceCriteria
    {
        public string? TeamName { get; set; }
    }
}
