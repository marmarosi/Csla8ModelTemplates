using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.WithCode
{
    /// <summary>
    /// Defines the data access functions of the read-only product choice collection.
    /// </summary>
    public interface IProductChoiceDal : ICodeNameChoiceDal<ProductChoiceCriteria>
    {
        new Task<List<CodeNameOptionDao>> FetchAsync(ProductChoiceCriteria criteria);
    }
}
