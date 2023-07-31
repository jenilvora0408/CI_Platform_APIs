namespace Entities.DTOs;

public class MissionDTO : MissionBaseDTO
{
    public DropdownDTO City { get; set; } = new DropdownDTO();

    public DropdownDTO Country { get; set; } = new DropdownDTO();

    public DropdownDTO MissionTheme { get; set; } = new DropdownDTO();

    public List<DropdownDTO> MissionSkills { get; set; } = new List<DropdownDTO>();

    public string? ThumbnailUrl { get; set; }

    public List<MediaDTO> Images { get; set; } = new List<MediaDTO>();

    public List<MediaDTO> Documents { get; set; } = new List<MediaDTO>();
}
