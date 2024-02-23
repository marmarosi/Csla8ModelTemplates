namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the data access functions of the read-only ID-name choice object.
    /// </summary>
    public interface IIdNameChoiceDal<T>
        where T : ChoiceCriteria
    {
        Task<List<IdNameOptionDao>> FetchAsync(T criteria);
    }
}
