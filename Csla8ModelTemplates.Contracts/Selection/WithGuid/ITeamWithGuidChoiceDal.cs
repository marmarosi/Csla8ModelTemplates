using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithGuid
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamWithGuidChoiceDal : IGuidNameChoiceDal<TeamWithGuidChoiceCriteria>
    {
        new Task<List<GuidNameOptionDao>> FetchAsync(TeamWithGuidChoiceCriteria criteria);
    }
}