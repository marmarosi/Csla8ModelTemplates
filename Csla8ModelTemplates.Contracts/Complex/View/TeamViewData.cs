namespace Csla8ModelTemplates.Contracts.Complex.View
{
    /// <summary>
    /// Defines the read-only team data.
    /// </summary>
    public abstract class TeamViewData
    {
        public string? TeamCode { get; set; }
        public string? TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only team object.
    /// </summary>
    public class TeamViewDao : TeamViewData
    {
        public long? TeamKey { get; set; }
        public required List<TeamViewPlayerDao> Players { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only team object.
    /// </summary>
    public class TeamViewDto : TeamViewData
    {
        public string? TeamId { get; set; }
        public required List<TeamViewPlayerDto> Players { get; set; }
    }
}
