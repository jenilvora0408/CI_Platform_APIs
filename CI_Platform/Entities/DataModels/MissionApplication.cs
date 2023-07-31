using Common.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;
public class MissionApplication : BaseEntity<long>
{   
    [Column("applied_on")]
    public DateTimeOffset AppliedOn { get; set; }

    [Column("status")]
    public MissionApplicationStatus Status { get; set; }

    [Column("volunteer_id")]
    public long VolunteerId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set;}

    [ForeignKey(nameof(MissionId))]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey(nameof(VolunteerId))]
    public virtual Volunteer Volunteer { get; set; } = null!;
}