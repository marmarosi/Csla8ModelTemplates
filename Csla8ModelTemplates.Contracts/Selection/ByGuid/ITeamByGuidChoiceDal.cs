using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.ByGuid
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamByGuidChoiceDal : IChoiceDal<Guid?, TeamByGuidChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<Guid?>>> FetchAsync(TeamByGuidChoiceCriteria criteria);
    }
}
