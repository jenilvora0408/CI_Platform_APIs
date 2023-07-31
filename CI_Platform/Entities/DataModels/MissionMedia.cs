using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;

public class MissionMedia
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("title")]
    [StringLength(255)]
    public string Name { get; set; } = null!;

    [Column("name")]
    [StringLength(1024)]
    public string Path { get; set; } = null!;

    [Column("type")]
    public string Type { get; set; } = null!;

    [Column("mission_id")]
    public long MissionId { get; set; }

    [ForeignKey(nameof(MissionId))]
    public virtual Mission Mission { get; set; } = null!;

    [Column("status")]
    public MediaStatus Status { get; set; }
}