using Common.Enums;
using Common.Utils;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;

public class Timesheet : BaseEntity<long>
{
    [Column("volunteer_id")]
    public long VolunteerId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("time")]
    public TimeSpan? Time { get; set; }

    [Column("goal_achieved")]
    public int? GoalAchieved { get; set; }

    [Column("date_volunteered")]
    public DateTimeOffset DateVolunteered { get; set; }

    [Column("notes")]
    public string? Notes { get; set; }

    [Column("status")]
    public TimesheetStatus Status { get; set; } = TimesheetStatus.PENDING;

    [ForeignKey(nameof(VolunteerId))]
    public virtual Volunteer Volunteer { get; set; } = null!;

    [ForeignKey(nameof(MissionId))]
    public virtual Mission Mission { get; set; } = null!;
}
