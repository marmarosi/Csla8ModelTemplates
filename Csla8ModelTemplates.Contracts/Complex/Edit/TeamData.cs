namespace Csla8ModelTemplates.Contracts.Complex.Edit
{
    /// <summary>
    /// Defines the editable team data.
    /// </summary>
    public abstract class TeamData
    {
        public string? TeamCode { get; set; }
        public string? TeamName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable team object.
    /// </summary>
    public class TeamDao : TeamData
    {
        public long? TeamKey { get; set; }
        public List<TeamPlayerDao> Players { get; set; }

        public TeamDao()
        {
            Players = new List<TeamPlayerDao>();
        }
    }

    /// <summary>
    /// Defines the data transfer object of the editable team object.
    /// </summary>
    public class TeamDto : TeamData
    {
        public string? TeamId { get; set; }
        public List<TeamPlayerDto> Players { get; set; }

        public TeamDto()
        {
            Players = new List<TeamPlayerDto>();
        }
    }
}
