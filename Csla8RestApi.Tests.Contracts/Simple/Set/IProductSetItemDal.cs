namespace Csla8RestApi.Tests.Contracts.Simple.Set
{
    /// <summary>
    /// Defines the data access functions of the editable product object.
    /// </summary>
    public interface IProductSetItemDal
    {
        Task InsertAsync(ProductSetItemDao dao);
        Task UpdateAsync(ProductSetItemDao dao);
        Task DeleteAsync(ProductSetItemCriteria criteria);
    }
}
