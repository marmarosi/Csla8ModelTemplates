using System.ComponentModel.DataAnnotations.Schema;

namespace Csla8ModelTemplates.Entities
{
    [Table("GroupPersons")]
    public class GroupPerson
    {
        public long? GroupKey { get; set; }

        [ForeignKey("GroupKey")]
        public virtual Group? Group { get; set; }

        public long? PersonKey { get; set; }

        [ForeignKey("PersonKey")]
        public virtual Person? Person { get; set; }
    }
}
