using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Common.Enums;

namespace Entities.DataModels;
public class Story : BaseEntity<long>
{
    [StringLength(255)]
    [Column("title")]
    public string Title { get; set; } = null!;

    [StringLength(1024)]
    [Column("short_description")]
    public string ShortDescription { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("status")]
    public StoryStatus Status { get; set; } = StoryStatus.PENDING;

    [Column("views")]
    public long Views { get; set; }

    [Column("published_at")]
    public DateTimeOffset? PublishedAt { get; set; }

    [Column("volunteer_id")]
    public long VolunteerId { get; set; }

    [Column("mission_id")]
    public long MissionId { get; set; }

    [ForeignKey(nameof(VolunteerId))]
    public Volunteer Volunteer { get; set; } = null!;

    [ForeignKey(nameof(MissionId))]
    public Mission Mission { get; set; } = null!;

    public virtual ICollection<StoryMedia> Medias { get; set; } = new List<StoryMedia>();
}