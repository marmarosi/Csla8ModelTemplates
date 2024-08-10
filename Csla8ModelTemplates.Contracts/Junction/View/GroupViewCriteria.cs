using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Junction.View
{
    /// <summary>
    /// Represents the criteria of the read-only group object.
    /// </summary>
    [Serializable]
    public class GroupViewCriteria
    {
        public long? GroupKey { get; set; }

        public GroupViewCriteria(
            string? groupId
            )
        {
            GroupKey = KeyHash.Decode(ID.Group, groupId);
        }
    }
}
