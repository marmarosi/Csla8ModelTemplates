using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Complex.Edit
{
    /// <summary>
    /// Represents the criteria of the editable team object.
    /// </summary>
    [Serializable]
    public class TeamCriteria
    {
        public long? TeamKey { get; set; }

        public TeamCriteria(
            string? teamId
            )
        {
            TeamKey = KeyHash.Decode(ID.Team, teamId);
        }
    }
}
