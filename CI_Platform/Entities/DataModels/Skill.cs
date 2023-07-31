using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;

public class Skill : BaseEntity<int>
{
    [Column("name")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("status")]
    public SkillStatus Status { get; set; }
}
