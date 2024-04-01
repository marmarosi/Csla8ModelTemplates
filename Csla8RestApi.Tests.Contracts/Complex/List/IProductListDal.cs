namespace Csla8RestApi.Tests.Contracts.Complex.List
{
    /// <summary>
    /// Defines the data access functions of the read-only product collection.
    /// </summary>
    public interface IProductListDal
    {
        Task<List<ProductListItemDao>> FetchAsync(ProductListCriteria criteria);
    }
}
