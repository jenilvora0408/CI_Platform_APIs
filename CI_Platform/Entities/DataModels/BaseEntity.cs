using Common.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class BaseEntity<T> : IdentityEntity<T>
    {
        [Column("created_on")]
        public DateTimeOffset CreatedOn { get; set; } = DateTimeProvider.GetCurrentDateTime();

        [Column("modified_on")]
        public DateTimeOffset ModifiedOn { get; set; } = DateTimeProvider.GetCurrentDateTime();

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
