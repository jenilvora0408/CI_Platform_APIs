namespace Entities.DTOs;

public class VolunteerMissionListRequestDTO : PageListRequestDTO
{
    public long CountryId { get; set; }

    public List<long> CityIds { get; set; } = new List<long>();

    public List<long> MissionThemeIds { get; set; } = new List<long>();

    public List<long> SkillIds { get; set; } = new List<long>();
}
