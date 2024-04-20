using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithId
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamWithIdChoiceDal : IIdNameChoiceDal<TeamWithIdChoiceCriteria>
    {
        new Task<List<IdNameOptionDao>> FetchAsync(TeamWithIdChoiceCriteria criteria);
    }
}
