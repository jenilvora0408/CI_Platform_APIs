using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class TimeBaseTimesheetFormDTO
{
    public long Id { get; set; }

    [Required]
    [Range(1, long.MaxValue, ErrorMessage = ModelStateConstant.TIMESHEET_MISSIONID)]
    public long MissionId { get; set; }

    [Required]
    [Range(0, 24, ErrorMessage = ModelStateConstant.TIMESHEET_HOUR)]
    public short Hours { get; set; }

    [Required]
    [MinuteValidation(nameof(Hours), ErrorMessage = ModelStateConstant.TIMESHEET_MINUTE)]
    public short Minute { get; set; }

    [Required]
    public DateTimeOffset DateVolunteered { get; set; }

    [MaxLength(1024, ErrorMessage = ModelStateConstant.TIMESHEET_NOTES)]
    public string? Notes { get; set; }
}
