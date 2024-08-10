using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("Roles")]
    public class Role : Timestamped
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? RoleKey { get; set; }

        [MaxLength(10)]
        public string? RoleCode { get; set; }

        [MaxLength(100)]
        public string? RoleName { get; set; }

        public virtual ICollection<UserRole>? Users { get; set; }
    }
}
