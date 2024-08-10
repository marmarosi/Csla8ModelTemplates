using Csla8RestApi.Dal;

namespace Csla8RestApi.Tests.Contracts.Junction.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable user object.
    /// </summary>
    public interface IUserDal : ITransactionalDal
    {
        Task<UserDao> FetchAsync(UserCriteria criteria);
        Task InsertAsync(UserDao dao);
        Task UpdateAsync(UserDao dao);
        Task DeleteAsync(UserCriteria criteria);
    }
}
