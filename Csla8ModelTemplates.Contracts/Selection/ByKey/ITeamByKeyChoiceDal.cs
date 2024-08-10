using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.ByKey
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamByKeyChoiceDal : IChoiceDal<long?, TeamByKeyChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<long?>>> FetchAsync(TeamByKeyChoiceCriteria criteria);
    }
}
