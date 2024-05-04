using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Junction.View
{
    /// <summary>
    /// Represents the criteria of the read-only user object.
    /// </summary>
    [Serializable]
    public class UserViewCriteria
    {
        public long? UserKey { get; set; }

        public UserViewCriteria(
            string? userId
            )
        {
            UserKey = KeyHash.Decode(ID.User, userId);
        }
    }
}
