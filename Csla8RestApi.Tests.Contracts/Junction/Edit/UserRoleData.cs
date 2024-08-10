namespace Csla8RestApi.Tests.Contracts.Junction.Edit
{
    /// <summary>
    /// Defines the editable user-role data.
    /// </summary>
    public abstract class UserRoleData
    {
        public string? RoleName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable user-role object.
    /// </summary>
    public class UserRoleDao : UserRoleData
    {
        public long? RoleKey { get; set; }
        public long? UserKey { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the editable user-role object.
    /// </summary>
    public class UserRoleDto : UserRoleData
    {
        public string? RoleId { get; set; }

        public UserRoleDto()
        { }

        public UserRoleDto(
            string roleId,
            string roleName
            )
        {
            RoleId = roleId;
            RoleName = roleName;
        }
    }
}
