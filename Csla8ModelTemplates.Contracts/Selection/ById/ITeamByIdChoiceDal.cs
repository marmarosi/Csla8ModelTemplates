using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.ById
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamByIdChoiceDal : IChoiceDal<long?, TeamByIdChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<long?>>> FetchAsync(TeamByIdChoiceCriteria criteria);
    }
}
