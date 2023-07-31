using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class CMSPageDTO
    {
        public long Id { get; set; }

        [Required]
        [StringLength(128)]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = ModelStateConstant.ONLY_ALPHABETS_ALLOWED)]
        public string Title { get; set; } = null!;

        [Required]
        [MinLength(255)]
        public string Description { get; set; } = null!;

        [Required]
        [StringLength(64)]
        public string Slug { get; set; } = null!;

        [EnumDataType(typeof(CMSPageStatus))]
        public CMSPageStatus? Status { get; set; }
    }
}
