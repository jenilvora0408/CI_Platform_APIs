using Common.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.DataModels;

public class Banner : BaseEntity<int>
{
    [Column("image")]
    [StringLength(1024)]
    public string Image { get; set; } = null!;

    [Column("sort_order")]
    public int SortOrder { get; set; }

    [Column("description")]
    [StringLength(4000)]
    public string Description { get; set; } = null!;

    [Column("status")]
    public BannerStatus Status { get; set; }
}
