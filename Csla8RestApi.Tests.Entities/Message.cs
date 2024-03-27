using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("Messages")]
    public class Message : Timestamped
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? MessageKey { get; set; }

        public long? ParentKey { get; set; }

        public long? RootKey { get; set; }

        public int? MessageOrder { get; set; }

        [MaxLength(100)]
        public string? MessageName { get; set; }

        [ForeignKey("ParentKey")]
        public virtual Message? Parent { get; set; }

        public virtual ICollection<Message>? Children { get; set; }
    }
}
