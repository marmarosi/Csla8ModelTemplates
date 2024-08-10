namespace Csla8RestApi.Tests.Contracts.Junction.View
{
    /// <summary>
    /// Defines the read-only user-role data.
    /// </summary>
    public abstract class UserViewRoleData
    {
        public string? RoleName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only user-role object.
    /// </summary>
    public class UserViewRoleDao : UserViewRoleData
    {
        public long? RoleKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only user-role object.
    /// </summary>
    public class UserViewRoleDto : UserViewRoleData
    {
        public string? RoleId { get; set; }
    }
}
