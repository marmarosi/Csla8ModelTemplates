using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithCode
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamWithCodeChoiceDal : ICodeNameChoiceDal<TeamWithCodeChoiceCriteria>
    {
        new Task<List<CodeNameOptionDao>> FetchAsync(TeamWithCodeChoiceCriteria criteria);
    }
}
