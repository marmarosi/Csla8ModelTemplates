using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Selection.WithGuid
{
    /// <summary>
    /// Defines the data access functions of the read-only team choice collection.
    /// </summary>
    public interface ITeamGuidChoiceDal : IGuidNameChoiceDal<TeamGuidChoiceCriteria>
    {
        new Task<List<GuidNameOptionDao>> FetchAsync(TeamGuidChoiceCriteria criteria);
    }
}
