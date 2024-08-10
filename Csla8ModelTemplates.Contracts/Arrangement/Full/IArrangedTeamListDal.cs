using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Arrangement.Full
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated sorted team collection.
    /// </summary>
    public interface IArrangedTeamListDal
    {
        Task<IPaginatedList<ArrangedTeamListItemDao>> FetchAsync(ArrangedTeamListCriteria criteria);
    }
}
