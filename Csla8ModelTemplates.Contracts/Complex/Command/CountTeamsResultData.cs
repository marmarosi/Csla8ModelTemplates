namespace Csla8ModelTemplates.Contracts.Complex.Command
{
    /// <summary>
    /// Defines the count teams list item data.
    /// </summary>
    public class CountTeamsResultData
    {
        public int PlayerCount { get; set; }
        public int TeamCountByPlayerCount { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the count teams list item object.
    /// </summary>
    public class CountTeamsResultDao : CountTeamsResultData
    { }

    /// <summary>
    /// Defines the data transfer object of the count teams list item object.
    /// </summary>
    public class CountTeamsResultDto : CountTeamsResultData
    { }
}
