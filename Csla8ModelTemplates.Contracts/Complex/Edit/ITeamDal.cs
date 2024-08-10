using Csla8RestApi.Dal;

namespace Csla8ModelTemplates.Contracts.Complex.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable team object.
    /// </summary>
    public interface ITeamDal : ITransactionalDal
    {
        Task<TeamDao> FetchAsync(TeamCriteria criteria);
        Task InsertAsync(TeamDao dao);
        Task UpdateAsync(TeamDao dao);
        Task DeleteAsync(TeamCriteria criteria);
    }
}
