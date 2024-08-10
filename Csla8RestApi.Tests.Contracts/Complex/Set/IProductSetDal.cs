using Csla8RestApi.Dal;

namespace Csla8RestApi.Tests.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the data access functions of the editable product collection.
    /// </summary>
    public interface IProductSetDal : ITransactionalDal
    {
        Task<List<ProductSetItemDao>> FetchAsync(ProductSetCriteria criteria);
    }
}
