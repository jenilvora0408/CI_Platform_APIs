using Common.Enums;

namespace Entities.DTOs;
public class BannerInfoDTO
{
    public long Id { get; set; } = 0;
    public string Image { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int SortOrder { get; set; }
    public BannerStatus Status { get; set; }
}