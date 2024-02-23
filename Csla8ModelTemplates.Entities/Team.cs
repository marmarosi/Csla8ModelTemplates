using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8ModelTemplates.Entities
{
    [Table("Teams")]
    public class Team : Timestamped
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? TeamKey { get; set; }

        public Guid? TeamGuid { get; set; }

        [MaxLength(10)]
        public string? TeamCode { get; set; }

        [MaxLength(100)]
        public string? TeamName { get; set; }

        public virtual ICollection<Player>? Players { get; set; }
    }
}
