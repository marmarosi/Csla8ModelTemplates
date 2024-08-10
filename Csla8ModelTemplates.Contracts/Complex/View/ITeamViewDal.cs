namespace Csla8ModelTemplates.Contracts.Complex.View
{
    /// <summary>
    /// Defines the data access functions of the read-only team object.
    /// </summary>
    public interface ITeamViewDal
    {
        Task<TeamViewDao> FetchAsync(TeamViewCriteria criteria);
    }
}
