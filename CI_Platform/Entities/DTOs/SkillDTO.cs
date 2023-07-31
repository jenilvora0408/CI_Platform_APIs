using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class SkillDTO
{
    public int Id { get; set; }

    [Required]
    [MaxLength(128, ErrorMessage = ModelStateConstant.SKILLNAME_LENGTH_ERROR)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EnumDataType(typeof(SkillStatus), ErrorMessage = ModelStateConstant.STATUS_INVALID)]
    public SkillStatus Status { get; set; }
}
