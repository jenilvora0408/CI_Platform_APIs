using Entities.CustomValidator;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

[CustomMissionValidator]
public class MissionFormDTO : MissionBaseDTO
{
    public long CityId { get; set; }

    public long MissionThemeId { get; set; }

    public List<int> MissionSkills { get; set; } = new List<int>();

    [Required]
    public IFormFile ThumbnailUrl { get; set; } = null!;

    public IFormFileCollection? Images { get; set; }

    public IFormFileCollection? Documents { get; set; }

    public List<long>? DeletedMedia { get; set; }
}
