using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.WithGuid
{
    /// <summary>
    /// Defines the data access functions of the read-only product choice collection.
    /// </summary>
    public interface IProductChoiceDal : IGuidNameChoiceDal<ProductChoiceCriteria>
    {
        new Task<List<GuidNameOptionDao>> FetchAsync(ProductChoiceCriteria criteria);
    }
}
