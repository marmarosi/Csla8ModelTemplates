namespace Csla8ModelTemplates.Contracts.Simple.View
{
    /// <summary>
    /// Defines the read-only team data.
    /// </summary>
    public class SimpleTeamViewData
    {
        public string TeamCode { get; set; }
        public string TeamName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only team model.
    /// </summary>
    public class SimpleTeamViewDao : SimpleTeamViewData
    {
        public long? TeamKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only team model.
    /// </summary>
    public class SimpleTeamViewDto : SimpleTeamViewData
    {
        public string TeamId { get; set; }
    }
}
