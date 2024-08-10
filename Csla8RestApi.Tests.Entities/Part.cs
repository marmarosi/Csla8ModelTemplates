using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("OrderLines")]
    public class Part
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? PartKey { get; set; }

        public long? ProductKey { get; set; }

        [MaxLength(10)]
        public string? PartCode { get; set; }

        [MaxLength(100)]
        public string? PartName { get; set; }

        [ForeignKey("PeoductKey")]
        public virtual Product? Peoduct { get; set; }
    }
}
