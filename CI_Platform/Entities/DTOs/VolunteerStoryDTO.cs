using Common.Constants;
using Common.Enums;
using Entities.CustomValidator;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

[CustomStoryValidationAttribute]
public class VolunteerStoryDTO
{
    public long Id { get; set; }

    [Required]
    [MaxLength(255, ErrorMessage = ModelStateConstant.STORY_TITLE_LENGTH)]
    public string Title { get; set; } = null!;

    [Required]
    [MaxLength(1024, ErrorMessage = ModelStateConstant.SHORT_DESC_LENGTH)]
    public string ShortDescription { get; set; } = null!;

    [Required]
    public string Description { get; set; } = null!;

    [EnumDataType(typeof(StoryStatus), ErrorMessage = ModelStateConstant.STATUS_INVALID)]
    public StoryStatus? Status { get; set; } = StoryStatus.PENDING;

    [Required]
    public long MissionId { get; set; }

    public IFormFileCollection? Medias { get; set; }

    public List<string>? MediaUrls { get; set; }

    public List<long>? DeletedMedia { get; set; }
}