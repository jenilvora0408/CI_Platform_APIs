using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required(ErrorMessage = ModelStateConstant.EMPTY_EMAIL_ERROR_MESSAGE)]
        public string Email { get; set; } = null!;
    }
}
