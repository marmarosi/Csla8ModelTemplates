namespace Csla8RestApi.Tests.Contracts.Junction.View
{
    /// <summary>
    /// Defines the read-only user data.
    /// </summary>
    public abstract class UserViewData
    {
        public string? UserCode { get; set; }
        public string? UserName { get; set; }
    }

    /// <summary>
    /// Defines the data access object of the read-only user object.
    /// </summary>
    public class UserViewDao : UserViewData
    {
        public long? UserKey { get; set; }
        public required List<UserViewRoleDao> Roles { get; set; }
    }

    /// <summary>
    /// Defines the data transfer object of the read-only user object.
    /// </summary>
    public class UserViewDto : UserViewData
    {
        public string? UserId { get; set; }
        public required List<UserViewRoleDto> Roles { get; set; }
    }
}
