using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("UserRoles")]
    public class UserRole
    {
        public long? UserKey { get; set; }

        [ForeignKey("UserKey")]
        public virtual User? User { get; set; }

        public long? RoleKey { get; set; }

        [ForeignKey("RoleKey")]
        public virtual Role? Role { get; set; }
    }
}
