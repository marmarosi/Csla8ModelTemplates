using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Complex.View
{
    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class TeamViewCriteria
    {
        public long? TeamKey { get; set; }

        public TeamViewCriteria(
            string? teamId
            )
        {
            TeamKey = KeyHash.Decode(ID.Team, teamId);
        }
    }
}
