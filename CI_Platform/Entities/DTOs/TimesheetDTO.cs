using Common.Enums;

namespace Entities.DTOs;

public class TimesheetDTO
{
    public long Id { get; set; }

    public string MissionTitle { get; set; } = string.Empty;

    public string VolunteerName { get; set; } = string.Empty;

    public DateTimeOffset DateVolunteered { get; set; }

    public MissionType Type { get; set; }

    public int? Contribution { get; set; }

    public short? Hours { get; set; }

    public short? Minutes { get; set; }
}
