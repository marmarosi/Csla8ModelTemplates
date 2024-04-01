using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.WithKey
{
    /// <summary>
    /// Defines the data access functions of the read-only product choice collection.
    /// </summary>
    public interface IProductChoiceDal : IKeyNameChoiceDal<ProductChoiceCriteria>
    {
        new Task<List<KeyNameOptionDao>> FetchAsync(ProductChoiceCriteria criteria);
    }
}
