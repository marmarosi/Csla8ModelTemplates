namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the data access functions of the read-only key-name choice object.
    /// </summary>
    public interface IKeyNameChoiceDal<T>
        where T : ChoiceCriteria
    {
        Task<List<KeyNameOptionDao>> FetchAsync(T criteria);
    }
}
