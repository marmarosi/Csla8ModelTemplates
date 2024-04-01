using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.WithId
{
    /// <summary>
    /// Defines the data access functions of the read-only product choice collection.
    /// </summary>
    public interface IProductChoiceDal : IIdNameChoiceDal<ProductChoiceCriteria>
    {
        new Task<List<IdNameOptionDao>> FetchAsync(ProductChoiceCriteria criteria);
    }
}
