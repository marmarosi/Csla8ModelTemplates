namespace Csla8RestApi.Tests.Contracts.Complex.Set
{
    /// <summary>
    /// Defines the data access functions of the editable part object.
    /// </summary>
    public interface IProductSetPartDal
    {
        Task InsertAsync(ProductSetPartDao dao);
        Task UpdateAsync(ProductSetPartDao dao);
        Task DeleteAsync(ProductSetPartCriteria criteria);
    }
}
