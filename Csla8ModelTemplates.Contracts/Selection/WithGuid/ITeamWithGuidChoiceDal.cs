using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithGuid
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamWithGuidChoiceDal : IChoiceDal<Guid?, TeamWithGuidChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<Guid?>>> FetchAsync(TeamWithGuidChoiceCriteria criteria);
    }
}
