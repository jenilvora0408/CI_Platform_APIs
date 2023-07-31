using Common.Enums;

namespace Entities.DTOs;

public class TimeBaseTimesheetDTO
{
    public DropdownDTO Mission { get; set; } = new DropdownDTO();

    public short Hours { get; set; }

    public short Minute { get; set; }

    public DateTimeOffset DateVolunteered { get; set; }

    public string? Notes { get; set; }

    public TimesheetStatus Status { get; set; } = TimesheetStatus.PENDING;
}
