using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.ByCode
{
    /// <summary>
    /// Represents the criteria of the read-only team choice collection.
    /// </summary>
    [Serializable]
    public class TeamByCodeChoiceCriteria : ChoiceCriteria
    {
        public string? TeamName { get; set; }
    }
}
