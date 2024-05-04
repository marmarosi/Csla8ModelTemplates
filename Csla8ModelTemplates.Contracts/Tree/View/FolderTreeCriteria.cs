using Csla8RestApi.Dal.Contracts;

namespace Csla8ModelTemplates.Contracts.Tree.View
{
    /// <summary>
    /// Represents the criteria of the read-only folder tree object.
    /// </summary>
    [Serializable]
    public class FolderTreeCriteria
    {
        public long? RootKey { get; set; }

        public FolderTreeCriteria(
            string? rootId
            )
        {
            RootKey = KeyHash.Decode(ID.Folder, rootId);
        }
    }
}
