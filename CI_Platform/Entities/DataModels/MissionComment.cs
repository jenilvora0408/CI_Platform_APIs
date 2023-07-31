using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class MissionComment : TimestampedEntity<long>
    {
        [Column("mission_id")]
        public long MissionId { get; set; }

        [StringLength(1024)]
        [Column("text")]
        public string Text { get; set; } = null!;

        [Column("volunteer_id")]
        public long VolunteerId { get; set; }

        [ForeignKey(nameof(MissionId))]
        public virtual Mission Mission { get; set; } = null!;

        [ForeignKey("VolunteerId")]
        public virtual Volunteer Volunteer { get; set; } = null!;

        [Column("status")]
        public CommentStatus Status { get; set; }
    }
}
