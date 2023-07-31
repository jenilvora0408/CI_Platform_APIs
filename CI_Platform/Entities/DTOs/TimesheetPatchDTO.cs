using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class TimesheetPatchDTO
{
    [Required(ErrorMessage = ExceptionMessage.TIMESHEET_STATUS_ERROR)]
    public TimesheetStatus Status { get; set; }
}
