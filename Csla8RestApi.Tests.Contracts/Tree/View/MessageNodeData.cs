namespace Csla8RestApi.Tests.Contracts.Tree.View
{
    /// <summary>
    /// Defines the read-only message node data.
    /// </summary>
    public class MessageNodeData
    {
        public int? MessageOrder { get; set; }
        public string? MessageName { get; set; }
        public int? Level { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only message node object.
    /// </summary>
    public class MessageNodeDao : MessageNodeData
    {
        public long? MessageKey { get; set; }
        public long? ParentKey { get; set; }
        public List<MessageNodeDao>? Children { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only message node object.
    /// </summary>
    public class MessageNodeDto : MessageNodeData
    {
        public string? MessageId { get; set; }
        public string? ParentId { get; set; }
        public List<MessageNodeDto>? Children { get; set; }
    }
}
