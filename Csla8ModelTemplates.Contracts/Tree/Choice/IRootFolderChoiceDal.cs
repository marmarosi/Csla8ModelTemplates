using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Tree.Choice
{
    /// <summary>
    /// Defines the data access functions of the read-only tree choice collection.
    /// </summary>
    public interface IRootFolderChoiceDal : IChoiceDal<long?, RootFolderChoiceCriteria>
    {
        new Task<List<ChoiceItemDao<long?>>> FetchAsync(RootFolderChoiceCriteria criteria);
    }
}
