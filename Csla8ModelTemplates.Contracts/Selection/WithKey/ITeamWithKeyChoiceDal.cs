using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithKey
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamWithKeyChoiceDal : IChoiceDal<long?, TeamWithKeyChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<long?>>> FetchAsync(TeamWithKeyChoiceCriteria criteria);
    }
}
