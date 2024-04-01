namespace Csla8RestApi.Tests.Contracts.Arrangement.Sorting
{
    /// <summary>
    /// Defines the data access functions of the read-only sorted product collection.
    /// </summary>
    public interface IProductListDal
    {
        Task<List<ProductListItemDao>> FetchAsync(ProductListCriteria criteria);
    }
}
