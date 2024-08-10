namespace Csla8ModelTemplates.Contracts.Arrangement.Sorting
{
    /// <summary>
    /// Defines the data access functions of the read-only sorted team collection.
    /// </summary>
    public interface ISortedTeamListDal
    {
        Task<List<SortedTeamListItemDao>> FetchAsync(SortedTeamListCriteria criteria);
    }
}
