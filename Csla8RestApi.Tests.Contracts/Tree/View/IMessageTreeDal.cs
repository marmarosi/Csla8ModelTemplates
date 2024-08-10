namespace Csla8RestApi.Tests.Contracts.Tree.View
{
    /// <summary>
    /// Defines the data access functions of the read-only message tree object.
    /// </summary>
    public interface IMessageTreeDal
    {
        Task<List<MessageNodeDao>> FetchAsync(MessageTreeCriteria criteria);
    }
}
