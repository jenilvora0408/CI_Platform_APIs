using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class Admin : AuditableEntity<long>
    {
        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;
    }
}