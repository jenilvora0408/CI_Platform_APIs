using Common.Constants;
using Common.Enums;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class CountryDTO
    {
        public long Id { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression("^[A-Za-z\\s]+$", ErrorMessage = ModelStateConstant.ONLY_ALPHABETS_ALLOWED)]
        public string Name { get; set; } = null!;

        [EnumDataType(typeof(CountryStatus))]
        public CountryStatus? Status { get; set; }
    }
}