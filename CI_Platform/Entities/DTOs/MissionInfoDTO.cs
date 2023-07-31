using Common.Enums;

namespace Entities.DTOs;
public class MissionInfoDTO
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string MissionTheme { get; set; } = string.Empty;

    public DateTimeOffset StartDate { get; set; }

    public MissionType MissionType { get; set; }

    public MissionStatus Status { get; set; }
}