using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Arrangement.Pagination
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated team collection.
    /// </summary>
    public interface IPaginatedTeamListDal
    {
        Task<IPaginatedList<PaginatedTeamListItemDao>> FetchAsync(PaginatedTeamListCriteria criteria);
    }
}
