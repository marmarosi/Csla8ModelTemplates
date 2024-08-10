using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Simple.Edit
{
    /// <summary>
    /// Represents the criteria of the editable team object.
    /// </summary>
    [Serializable]
    public class SimpleTeamCriteria
    {
        public long? TeamKey { get; set; }

        public SimpleTeamCriteria(
            string? teamId
            )
        {
            TeamKey = KeyHash.Decode(ID.Team, teamId);
        }
    }
}
