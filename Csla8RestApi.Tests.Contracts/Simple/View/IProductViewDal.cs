namespace Csla8RestApi.Tests.Contracts.Simple.View
{
    /// <summary>
    /// Defines the data access functions of the read-only product model.
    /// </summary>
    public interface IProductViewDal
    {
        Task<ProductViewDao> FetchAsync(ProductViewCriteria criteria);
    }
}
