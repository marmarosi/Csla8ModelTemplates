using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Junction.Edit
{
    /// <summary>
    /// Represents the criteria of the editable user object.
    /// </summary>
    [Serializable]
    public class UserParams
    {
        public string UserId { get; set; }

        public UserParams(
            string userId
            )
        {
            UserId = userId;
        }

        public UserCriteria Decode()
        {
            return new UserCriteria
            {
                UserKey = KeyHash.Decode(ID.User, UserId) ?? 0
            };
        }
    }

    /// <summary>
    /// Represents the criteria of the editable user object.
    /// </summary>
    [Serializable]
    public class UserCriteria
    {
        public long? UserKey { get; set; }

        public UserCriteria() { }

        public UserCriteria(
            long? userKey
            )
        {
            UserKey = userKey;
        }
    }
}
