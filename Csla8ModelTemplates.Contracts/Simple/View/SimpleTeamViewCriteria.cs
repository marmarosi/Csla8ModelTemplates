using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Simple.View
{
    /// <summary>
    /// Represents the criteria of the read-only team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamViewCriteria
    {
        public long? TeamKey { get; set; }

        public SimpleTeamViewCriteria(
            string? teamId
            )
        {
            TeamKey = KeyHash.Decode(ID.Team, teamId);
        }
    }
}
