namespace Csla8ModelTemplates.Contracts.Junction.View
{
    /// <summary>
    /// Defines the data access functions of the read-only group object.
    /// </summary>
    public interface IGroupViewDal
    {
        Task<GroupViewDao> FetchAsync(GroupViewCriteria criteria);
    }
}
