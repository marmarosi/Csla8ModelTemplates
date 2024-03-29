namespace Csla8ModelTemplates.Contracts.Complex.List
{
    /// <summary>
    /// Defines the data access functions of the read-only team collection.
    /// </summary>
    public interface ITeamListDal
    {
        Task<List<TeamListItemDao>> FetchAsync(TeamListCriteria criteria);
    }
}
