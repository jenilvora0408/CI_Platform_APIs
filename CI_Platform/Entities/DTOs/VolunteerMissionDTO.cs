using Common.Enums;
using Entities.DataModels;

namespace Entities.DTOs
{
    public class VolunteerMissionDTO
    {
        public long Id { get; set; }

        public string? ThumbnailUrl { get; set; }

        public string Title { get; set; } = null!;

        public string ShortDescription { get; set; } = null!;

        public string Description { get; set; } = null!;

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset? EndDate { get; set; }

        public DateTimeOffset? RegistrationDeadline { get; set; }

        public long? RemainingSeat { get; set; }

        public MissionType MissionType { get; set; }

        public string? OrganizationName { get; set; }

        public string? OrganizationDetails { get; set; }

        public AvailabilityType? Availability { get; set; }

        public string? GoalObjectiveTitle { get; set; }

        public long? GoalObjective { get; set; }

        public long? GoalObjectiveAchieved { get; set; }

        public string CityName { get; set; } = null!;

        public string ThemeTitle { get; set; } = null!;

        public double AvgRating { get; set; } = 0;

        public bool IsFavourite { get; set; } = false;

        public bool HasApplied { get; set; } = false;

        public List<DropdownDTO> SkillList { get; set; } = new List<DropdownDTO>();
    }
}
