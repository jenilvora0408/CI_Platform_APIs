using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class FavouriteMission : TimestampedEntity<long>
    {
        [Column("mission_id")]
        public long MissionId { get; set; }

        [Column("is_favourite")]
        public bool IsFavourite { get; set; } = true;

        [Column("volunteer_id")]
        public long VolunteerId { get; set; }

        [ForeignKey(nameof(MissionId))]
        public virtual Mission Mission { get; set; } = null!;

        [ForeignKey("VolunteerId")]
        public virtual Volunteer Volunteer { get; set; } = null!;
    }
}