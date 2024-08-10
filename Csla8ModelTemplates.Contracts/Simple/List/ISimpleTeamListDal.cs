namespace Csla8ModelTemplates.Contracts.Simple.List
{
    /// <summary>
    /// Defines the data access functions of the read-only team collection.
    /// </summary>
    public interface ISimpleTeamListDal
    {
        Task<List<SimpleTeamListItemDao>> FetchAsync(SimpleTeamListCriteria criteria);
    }
}
