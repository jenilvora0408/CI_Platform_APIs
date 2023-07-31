using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class MissionThemeDTO
    {
        public long Id { get; set; }

        [Required]  
        [StringLength(255)]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = ModelStateConstant.ONLY_ALPHABETS_ALLOWED)]
        public string Title { get; set; } = null!;

        [EnumDataType(typeof(MissionThemeStatus))]
        public MissionThemeStatus? Status { get; set; }
    }
}
