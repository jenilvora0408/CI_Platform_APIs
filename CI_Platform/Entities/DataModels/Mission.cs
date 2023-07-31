using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels
{
    public class Mission : BaseEntity<long>
    {
        [StringLength(255)]
        [Column("title")]
        public string Title { get; set; } = null!;

        [StringLength(1024)]
        [Column("short_description")]
        public string ShortDescription { get; set; } = null!;

        [Column("description")]
        public string Description { get; set; } = null!;

        [Column("start_date")]
        public DateTimeOffset StartDate { get; set; }

        [Column("end_date")]
        public DateTimeOffset? EndDate { get; set; }

        [Column("registration_deadline")]
        public DateTimeOffset? RegistrationDeadline { get; set; }

        [Column("total_seat")]
        public int? TotalSeat { get; set; }

        [Column("mission_type")]
        public MissionType MissionType { get; set; }

        [StringLength(128)]
        [Column("organization_name")]
        public string? OrganizationName { get; set; }

        [Column("organization_details")]
        public string? OrganizationDetails { get; set; }

        [Column("status")]
        public MissionStatus Status { get; set; }

        [Column("availability")]
        public AvailabilityType? Availability { get; set; }

        [Column("city_id")]
        public long CityId { get; set; }

        [Column("mission_theme_id")]
        public long MissionThemeId { get; set; }

        [Column("goal_objective_title")]
        [StringLength(128)]
        public string? GoalObjectiveTitle { get; set; }

        [Column("goal_objective")]
        public long? GoalObjective { get; set; }

        [Column("goal_objective_achieved")]
        public long? GoalObjectiveAchieved { get; set; }

        [Column("thumbnail_url")]
        [StringLength(1024)]
        public string ThumbnailUrl { get; set; } = null!;

        [ForeignKey(nameof(CityId))]
        public virtual City City { get; set; } = null!;

        [ForeignKey(nameof(MissionThemeId))]
        public virtual MissionTheme MissionTheme { get; set; } = null!;

        public virtual List<MissionMedia> Medias { get; set; } = new List<MissionMedia>();

        public virtual List<MissionSkills> Skills { get; set; } = new List<MissionSkills>();

        public virtual ICollection<MissionRating> MissionRating { get; set; } = new List<MissionRating>();

        public virtual ICollection<FavouriteMission> FavouriteMissions { get; } = new List<FavouriteMission>();

        public virtual ICollection<MissionApplication> MissionApplications { get; } = new List<MissionApplication>();
        
    }
}