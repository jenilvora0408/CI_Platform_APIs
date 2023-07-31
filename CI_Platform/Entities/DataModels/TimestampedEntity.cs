using Common.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class TimestampedEntity<T> : IdentityEntity<T>
    {
        [Column("created_on")]
        public DateTimeOffset CreatedOn { get; set; } = DateTimeProvider.GetCurrentDateTime();

        [Column("modified_on")]
        public DateTimeOffset ModifiedOn { get; set; } = DateTimeProvider.GetCurrentDateTime();
    }
}
