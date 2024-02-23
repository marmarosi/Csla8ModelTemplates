namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the data access functions of the read-only Guid-name choice object.
    /// </summary>
    public interface IGuidNameChoiceDal<T>
        where T : ChoiceCriteria
    {
        Task<List<GuidNameOptionDao>> FetchAsync(T criteria);
    }
}
