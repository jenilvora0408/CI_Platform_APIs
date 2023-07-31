using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Entities.DataModels;
public class BaseMedia
{
    [Key]
    [Column("id")]
    public long Id { get; set; }

    [Column("path")]
    [StringLength(1024)]
    public string Path { get; set; } = null!;

    [Column("type")]
    public string Type { get; set; } = null!;

    [Column("status")]
    public MediaStatus Status { get; set; }
}