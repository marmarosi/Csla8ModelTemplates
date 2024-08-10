using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8ModelTemplates.Entities
{
    [Table("Folders")]
    public class Folder : Timestamped
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? FolderKey { get; set; }

        public long? ParentKey { get; set; }

        public long? RootKey { get; set; }

        public int? FolderOrder { get; set; }

        [MaxLength(100)]
        public string? FolderName { get; set; }

        [ForeignKey("ParentKey")]
        public virtual Folder? Parent { get; set; }

        public virtual ICollection<Folder>? Children { get; set; }
    }
}
