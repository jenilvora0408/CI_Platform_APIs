using Common.Enums;

namespace Entities.DTOs;

public class GoalBaseTimesheetDTO
{
    public DropdownDTO Mission { get; set; } = new DropdownDTO();

    public int Contribution { get; set; }

    public DateTimeOffset DateVolunteered { get; set; }

    public string? Notes { get; set; }

    public TimesheetStatus Status { get; set; } = TimesheetStatus.PENDING;
}
