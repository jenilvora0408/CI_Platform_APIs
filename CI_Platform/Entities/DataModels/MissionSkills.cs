using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;

public class MissionSkills
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [Column("skill_id")]
    public int SkillId { get; set; }

    [ForeignKey(nameof(MissionId))]
    public virtual Mission Mission { get; set; } = null!;

    [ForeignKey(nameof(SkillId))]
    public virtual Skill Skill { get; set; } = null!;

    [Column("status")]
    public SkillStatus Status { get; set; }
}