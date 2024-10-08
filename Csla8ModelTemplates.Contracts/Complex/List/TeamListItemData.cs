namespace Csla8ModelTemplates.Contracts.Complex.List
{
    /// <summary>
    /// Defines the read-only team list item data.
    /// </summary>
    public abstract class TeamListItemData
    {
        public string? TeamCode { get; set; }
        public string? TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only team list item object.
    /// </summary>
    public class TeamListItemDao : TeamListItemData
    {
        public long? TeamKey { get; set; }
        public required List<TeamListPlayerDao> Players { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only team list item object.
    /// </summary>
    public class TeamListItemDto : TeamListItemData
    {
        public string? TeamId { get; set; }
        public required List<TeamListPlayerDto> Players { get; set; }
    }
}
