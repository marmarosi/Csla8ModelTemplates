using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Tree.View
{
    /// <summary>
    /// Represents the criteria of the read-only message tree object.
    /// </summary>
    [Serializable]
    public class MessageTreeCriteria
    {
        public long? RootKey { get; set; }

        public MessageTreeCriteria(
            string? rootId
            )
        {
            RootKey = KeyHash.Decode(ID.Message, rootId);
        }
    }
}
