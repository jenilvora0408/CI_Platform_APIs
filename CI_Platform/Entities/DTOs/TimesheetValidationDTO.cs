using Common.Enums;

namespace Entities.DTOs;

public class TimesheetValidationDTO
{
    public long RemainingGoal { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public MissionType MissionType { get; set; }
}
