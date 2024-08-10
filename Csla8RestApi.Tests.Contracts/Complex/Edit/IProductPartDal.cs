namespace Csla8RestApi.Tests.Contracts.Complex.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable part object.
    /// </summary>
    public interface IProductPartDal
    {
        Task InsertAsync(ProductPartDao dao);
        Task UpdateAsync(ProductPartDao dao);
        Task DeleteAsync(ProductPartCriteria criteria);
    }
}
