namespace Csla8ModelTemplates.Contracts.Tree.View
{
    /// <summary>
    /// Defines the data access functions of the read-only folder tree object.
    /// </summary>
    public interface IFolderTreeDal
    {
        Task<List<FolderNodeDao>> FetchAsync(FolderTreeCriteria criteria);
    }
}
