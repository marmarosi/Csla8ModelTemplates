using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Selection.ByCode
{
    /// <summary>
    /// Defines the data access functions of the read-only product choice collection.
    /// </summary>
    public interface IProductChoiceDal : IChoiceDal<string?, ProductChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<string?>>> FetchAsync(ProductChoiceCriteria criteria);
    }
}
