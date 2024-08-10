namespace Csla8RestApi.Tests.Contracts.Complex.View
{
    /// <summary>
    /// Defines the data access functions of the read-only product object.
    /// </summary>
    public interface IProductViewDal
    {
        Task<ProductViewDao> FetchAsync(ProductViewCriteria criteria);
    }
}
