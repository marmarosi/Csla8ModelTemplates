namespace Csla8RestApi.Tests.Contracts.Junction.Edit
{
    /// <summary>
    /// Defines the data access functions of the editable user-role object.
    /// </summary>
    public interface IUserRoleDal
    {
        Task InsertAsync(UserRoleDao dao);
        Task DeleteAsync(UserRoleDao dao);
    }
}
