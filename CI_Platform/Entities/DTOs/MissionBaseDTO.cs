using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class MissionBaseDTO
{
    public long Id { get; set; }

    [Required]
    [MaxLength(255, ErrorMessage = ModelStateConstant.MISSION_TITLE_LENGTH)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(1024, ErrorMessage = ModelStateConstant.SHORT_DESC_LENGTH)]
    public string ShortDescription { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [Required]
    public DateTimeOffset? StartDate { get; set; }

    public DateTimeOffset? EndDate { get; set; }

    public DateTimeOffset? RegistrationDeadline { get; set; }

    public long? TotalSeat { get; set; }

    [EnumDataType(typeof(MissionType), ErrorMessage = ModelStateConstant.MISSION_TYPE)]
    public MissionType MissionType { get; set; }

    [MaxLength(128)]
    public string? OrganizationName { get; set; }

    public string? OrganizationDetails { get; set; }

    [EnumDataType(typeof(MissionStatus), ErrorMessage = ModelStateConstant.STATUS_INVALID)]
    public MissionStatus Status { get; set; }

    [EnumDataType(typeof(AvailabilityType), ErrorMessage = ModelStateConstant.AVAILABILTY)]
    public AvailabilityType? Availability { get; set; }

    [MaxLength(128, ErrorMessage = ModelStateConstant.GOAL_OBJECTIVE_TITLE)]
    public string? GoalObjectiveTitle { get; set; }

    public long? GoalObjective { get; set; }
}
