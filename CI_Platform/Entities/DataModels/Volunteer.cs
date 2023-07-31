using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class Volunteer : AuditableEntity<long>
    {
        [Column("phone_number")]
        [StringLength(20, MinimumLength = 7)]
        public string PhoneNumber { get; set; } = null!;

        [Column("employee_id")]
        [StringLength(8)]
        public string? EmployeeId { get; set; }

        [Column("department")]
        [StringLength(20)]
        public string? Department { get; set; }

        [Column("profile_text")]
        [StringLength(1024)]
        public string? ProfileText { get; set; }

        [Column("availability")]
        public AvailabilityType Availability { get; set; }

        [Column("reason_to_be_volunteer")]
        [StringLength(4000)]
        public string? ReasonToBeVolunteer { get; set; }

        [Column("city_id")]
        public long CityId { get; set; }

        [ForeignKey("CityId")]
        public virtual City City { get; set; } = null!;

        [Column("user_id")]
        public long UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; } = null!;

        public virtual List<VolunteerSkills> Skills { get; } = new List<VolunteerSkills>();

        public virtual ICollection<MissionRating> MissionRating { get; } = new List<MissionRating>();

        public virtual ICollection<FavouriteMission> FavouriteMissions { get; } = new List<FavouriteMission>();
    }
}
