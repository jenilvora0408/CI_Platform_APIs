using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class MissionRating : TimestampedEntity<long>
    {
        [Column("mission_id")]
        public long MissionId { get; set; }

        [Column("rating")]
        public int Rating { get; set; }

        [Column("volunteer_id")]
        public long VolunteerId { get; set; }

        [ForeignKey(nameof(MissionId))]
        public virtual Mission Mission { get; set; } = null!;

        [ForeignKey("VolunteerId")]
        public virtual Volunteer Volunteer { get; set; } = null!;
    }
}