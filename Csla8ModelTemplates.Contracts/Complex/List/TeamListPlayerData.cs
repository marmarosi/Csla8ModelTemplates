namespace Csla8ModelTemplates.Contracts.Complex.List
{
    /// <summary>
    /// Defines the read-only player list item data.
    /// </summary>
    public abstract class TeamListPlayerData
    {
        public string? PlayerCode { get; set; }
        public string? PlayerName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only player object.
    /// </summary>
    public class TeamListPlayerDao : TeamListPlayerData
    {
        public long? PlayerKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only player object.
    /// </summary>
    public class TeamListPlayerDto : TeamListPlayerData
    {
        public string? PlayerId { get; set; }
    }
}
