using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.ById
{
    /// <summary>
    /// Defines the data access functions of the read-only product choice collection.
    /// </summary>
    public interface IProductChoiceDal : IChoiceDal<long?, ProductChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<long?>>> FetchAsync(ProductChoiceCriteria criteria);
    }
}
