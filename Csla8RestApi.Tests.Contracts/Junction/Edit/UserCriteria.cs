using Csla8RestApi.Dal.Contracts;

namespace Csla8RestApi.Tests.Contracts.Junction.Edit
{
    /// <summary>
    /// Represents the criteria of the editable user object.
    /// </summary>
    [Serializable]
    public class UserCriteria
    {
        public long? UserKey { get; set; }

        public UserCriteria(
            string? userId
            )
        {
            UserKey = KeyHash.Decode(ID.User, userId);
        }
    }
}
