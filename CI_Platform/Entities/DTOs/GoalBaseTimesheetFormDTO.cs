using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class GoalBaseTimesheetFormDTO
{
    public long Id { get; set; }

    [Required]
    [Range(1, long.MaxValue, ErrorMessage = ModelStateConstant.TIMESHEET_MISSIONID)]
    public long MissionId { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = ModelStateConstant.TIMESHEET_GOALACHIEVED)]
    public int Contribution { get; set; }

    [Required]
    public DateTimeOffset DateVolunteered { get; set; }

    [MaxLength(1024, ErrorMessage = ModelStateConstant.TIMESHEET_NOTES)]
    public string? Notes { get; set; }
}
