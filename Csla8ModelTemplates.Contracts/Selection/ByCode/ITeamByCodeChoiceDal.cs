using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.ByCode
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamByCodeChoiceDal : IChoiceDal<string?, TeamByCodeChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<string?>>> FetchAsync(TeamByCodeChoiceCriteria criteria);
    }
}
