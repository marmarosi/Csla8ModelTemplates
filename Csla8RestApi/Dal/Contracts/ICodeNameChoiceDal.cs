namespace Csla8RestApi.Dal.Contracts
{
    /// <summary>
    /// Defines the data access functions of the read-only code-name choice object.
    /// </summary>
    public interface ICodeNameChoiceDal<T>
        where T : ChoiceCriteria
    {
        Task<List<CodeNameOptionDao>> FetchAsync(T criteria);
    }
}
