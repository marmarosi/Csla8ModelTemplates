using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Junction.Edit
{
    /// <summary>
    /// Represents the criteria of the editable group object.
    /// </summary>
    [Serializable]
    public class GroupCriteria
    {
        public long? GroupKey { get; set; }

        public GroupCriteria(
            string? groupId
            )
        {
            GroupKey = KeyHash.Decode(ID.Group, groupId);
        }
    }
}
