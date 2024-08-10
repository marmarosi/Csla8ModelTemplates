using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.ByGuid
{
    /// <summary>
    /// Defines the data access functions of the read-only product choice collection.
    /// </summary>
    public interface IProductChoiceDal : IChoiceDal<Guid?, ProductChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<Guid?>>> FetchAsync(ProductChoiceCriteria criteria);
    }
}
