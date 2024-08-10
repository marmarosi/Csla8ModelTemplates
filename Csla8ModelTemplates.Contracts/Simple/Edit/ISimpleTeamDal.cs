using Csla8RestApi.Dal;

namespace Csla8ModelTemplates.Contracts.Simple.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ISimpleTeamDal : ITransactionalDal
    {
        Task<SimpleTeamDao> FetchAsync(SimpleTeamCriteria criteria);
        Task InsertAsync(SimpleTeamDao dao);
        Task UpdateAsync(SimpleTeamDao dao);
        Task DeleteAsync(SimpleTeamCriteria criteria);
    }
}
