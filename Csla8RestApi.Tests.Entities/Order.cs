using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("Orders")]
    public class Order : Timestamped
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? OrderKey { get; set; }

        public Guid? OrderGuid { get; set; }

        [MaxLength(10)]
        public string? OrderCode { get; set; }

        [MaxLength(100)]
        public string? OrderName { get; set; }

        public virtual ICollection<OrderLine>? OrderLines { get; set; }
    }
}
