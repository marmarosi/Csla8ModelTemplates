namespace Csla8RestApi.Tests.Contracts.Junction.Edit
{
    /// <summary>
    /// Defines the editable user data.
    /// </summary>
    public class UserData
    {
        public string? UserCode { get; set; }
        public string? UserName { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the editable user object.
    /// </summary>
    public class UserDao : UserData
    {
        public long? UserKey { get; set; }
        public List<UserRoleDao> Roles { get; set; }

        public UserDao()
        {
            Roles = new List<UserRoleDao>();
        }
    }

    /// <summary>
    /// Defines the data transfer object of the editable user object.
    /// </summary>
    public class UserDto : UserData
    {
        public string? UserId { get; set; }
        public List<UserRoleDto> Roles { get; set; }

        public UserDto()
        {
            Roles = new List<UserRoleDto>();
        }
    }
}
