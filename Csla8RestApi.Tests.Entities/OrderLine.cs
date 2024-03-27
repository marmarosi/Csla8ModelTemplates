using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8RestApi.Tests.Entities
{
    [Table("OrderLines")]
    public class OrderLine
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long? OrderLineKey { get; set; }

        public long? OrderKey { get; set; }

        [MaxLength(10)]
        public string? OrderLineCode { get; set; }

        [MaxLength(100)]
        public string? OrderLineName { get; set; }

        [ForeignKey("OrderKey")]
        public virtual Order? Order { get; set; }
    }
}
