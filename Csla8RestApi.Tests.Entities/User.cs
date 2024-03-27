using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("Users")]
    public class User : Timestamped
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? UserKey { get; set; }

        [MaxLength(10)]
        public string? UserCode { get; set; }

        [MaxLength(100)]
        public string? UserName { get; set; }

        public virtual ICollection<UserRole>? Roles { get; set; }
    }
}
