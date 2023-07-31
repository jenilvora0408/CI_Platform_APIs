using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ResetPasswordDTO
    {
        [Required]
        [StringLength(15, MinimumLength = 8, ErrorMessage = ModelStateConstant.PASSWORD_LENGTH_ERROR_MESSAGE)]
        [RegularExpression(ModelStateConstant.PASSWORD_POLICY, ErrorMessage = ModelStateConstant.PASSWORD_ERROR_MESSAGE)]
        public string Password { get; set; } = null!;
    }
}
