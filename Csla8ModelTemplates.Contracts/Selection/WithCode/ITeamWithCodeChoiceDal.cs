using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithCode
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamWithCodeChoiceDal : IChoiceDal<string?, TeamWithCodeChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<string?>>> FetchAsync(TeamWithCodeChoiceCriteria criteria);
    }
}
