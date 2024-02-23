using Csla8RestApi.Dal;

namespace Csla8ModelTemplates.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the data access functions of the editable team collection.
    /// </summary>
    public interface ITeamSetDal : ITransactionalDal
    {
        Task<List<TeamSetItemDao>> FetchAsync(TeamSetCriteria criteria);
    }
}
