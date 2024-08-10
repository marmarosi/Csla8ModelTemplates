namespace Csla8RestApi.Tests.Contracts.Junction.View
{
    /// <summary>
    /// Defines the data access functions of the read-only user object.
    /// </summary>
    public interface IUserViewDal
    {
        Task<UserViewDao> FetchAsync(UserViewCriteria criteria);
    }
}
