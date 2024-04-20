using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithKey
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamWithKeyChoiceDal : IKeyNameChoiceDal<TeamWithKeyChoiceCriteria>
    {
        new Task<List<KeyNameOptionDao>> FetchAsync(TeamWithKeyChoiceCriteria criteria);
    }
}
