using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;

public class VolunteerSkills
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("volunteer_id")]
    public long VolunteerId { get; set; }

    [Column("skill_id")]
    public int SkillId { get; set; }

    [ForeignKey(nameof(VolunteerId))]
    public virtual Volunteer Volunteer { get; set; } = null!;

    [ForeignKey(nameof(SkillId))]
    public virtual Skill Skill { get; set; } = null!;

    [Column("status")]
    public SkillStatus Status { get; set; }
}
