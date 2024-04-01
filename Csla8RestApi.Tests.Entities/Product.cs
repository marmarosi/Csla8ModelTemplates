using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("Products")]
    public class Product : Timestamped
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? ProductKey { get; set; }

        public Guid? ProductGuid { get; set; }

        [MaxLength(10)]
        public string? ProductCode { get; set; }

        [MaxLength(100)]
        public string? ProductName { get; set; }

        public virtual ICollection<Part>? Parts { get; set; }
    }
}
