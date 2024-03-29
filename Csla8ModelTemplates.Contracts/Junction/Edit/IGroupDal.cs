using Csla8RestApi.Dal;

namespace Csla8ModelTemplates.Contracts.Junction.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable group object.
    /// </summary>
    public interface IGroupDal : ITransactionalDal
    {
        Task<GroupDao> FetchAsync(GroupCriteria criteria);
        Task InsertAsync(GroupDao dao);
        Task UpdateAsync(GroupDao dao);
        Task DeleteAsync(GroupCriteria criteria);
    }
}
