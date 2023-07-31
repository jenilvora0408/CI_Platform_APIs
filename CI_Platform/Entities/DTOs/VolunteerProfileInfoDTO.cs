namespace Entities.DTOs;

public class VolunteerProfileInfoDTO : VolunteerProfileFormDTO
{
    public List<DropdownDTO> SkillsList { get; set; } = new List<DropdownDTO>();

    public long CountryId { get; set; }

    public string Avatar { get; set; } = string.Empty;
}
