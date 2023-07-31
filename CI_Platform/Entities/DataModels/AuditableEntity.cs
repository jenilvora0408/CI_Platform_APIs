using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class AuditableEntity<T> : IdentityEntity<T>
    {
        [Column("created_by")]
        public long CreatedBy { get; set; }

        [Column("modified_by")]
        public long ModifiedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual User? CreatedByUser { get; set; }

        [ForeignKey("ModifiedBy")]
        public virtual User? ModifiedByUser { get; set; }
    }
}
