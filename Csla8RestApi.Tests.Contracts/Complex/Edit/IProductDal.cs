using Csla8RestApi.Dal;

namespace Csla8RestApi.Tests.Contracts.Complex.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable product object.
    /// </summary>
    public interface IProductDal : ITransactionalDal
    {
        Task<ProductDao> FetchAsync(ProductCriteria criteria);
        Task InsertAsync(ProductDao dao);
        Task UpdateAsync(ProductDao dao);
        Task DeleteAsync(ProductCriteria criteria);
    }
}
