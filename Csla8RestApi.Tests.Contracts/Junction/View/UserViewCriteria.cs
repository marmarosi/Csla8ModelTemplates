using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Junction.View
{
    /// <summary>
    /// Represents the criteria of the read-only user object.
    /// </summary>
    [Serializable]
    public class UserViewParams
    {
        public string? UserId { get; set; }

        public UserViewParams(
            string userId
            )
        {
            UserId = userId;
        }

        public UserViewCriteria Decode()
        {
            return new UserViewCriteria
            {
                UserKey = KeyHash.Decode(ID.User, UserId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the read-only user object.
    /// </summary>
    [Serializable]
    public class UserViewCriteria
    {
        public long? UserKey { get; set; }
    }
}
