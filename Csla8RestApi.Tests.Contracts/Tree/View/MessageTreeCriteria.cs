using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Tree.View
{
    /// <summary>
    /// Represents the criteria of the read-only message tree object.
    /// </summary>
    [Serializable]
    public class MessageTreeParams
    {
        public string RootId { get; set; }

        public MessageTreeParams(
            string rootId
            )
        {
            RootId = rootId;
        }

        public MessageTreeCriteria Decode()
        {
            return new MessageTreeCriteria
            {
                RootKey = KeyHash.Decode(ID.Message, RootId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the read-only message tree object.
    /// </summary>
    [Serializable]
    public class MessageTreeCriteria
    {
        public long RootKey { get; set; }
    }
}
