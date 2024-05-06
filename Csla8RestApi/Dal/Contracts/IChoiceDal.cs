namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the data access functions of the read-only choice object.
    /// </summary>
    public interface IChoiceDal<T, C>
        where C : ChoiceCriteria
    {
        Task<List<ChoiceItemDao<T>>> FetchAsync(C criteria);
    }
}
