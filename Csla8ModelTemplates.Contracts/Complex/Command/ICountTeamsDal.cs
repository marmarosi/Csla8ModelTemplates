namespace Csla8ModelTemplates.Contracts.Complex.Command
{
    /// <summary>
    /// Defines the data access functions of the count teams by player count command.
    /// </summary>
    public interface ICountTeamsDal
    {
        Task<List<CountTeamsResultDao>> ExecuteAsync(CountTeamsCriteria criteria);
    }
}
