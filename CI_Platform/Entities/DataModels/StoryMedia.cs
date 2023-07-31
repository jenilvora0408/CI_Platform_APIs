using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;

public class StoryMedia : BaseMedia
{
    [Column("story_id")]
    public long StoryId { get; set; }

    [ForeignKey(nameof(StoryId))]
    public virtual Story Story { get; set; } = null!;
}