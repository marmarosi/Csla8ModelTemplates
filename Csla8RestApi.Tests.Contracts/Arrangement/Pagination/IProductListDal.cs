using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Arrangement.Pagination
{
    /// <summary>
    /// Defines the data access functions of the read-only paginated product collection.
    /// </summary>
    public interface IProductListDal
    {
        Task<IPaginatedList<ProductListItemDao>> FetchAsync(ProductListCriteria criteria);
    }
}
