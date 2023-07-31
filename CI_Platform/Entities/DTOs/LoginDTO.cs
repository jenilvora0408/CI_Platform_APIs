using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;
public class LoginDTO
{
    [Required(ErrorMessage = ModelStateConstant.EMAIL_ERROR_MESSAGE)]
    [RegularExpression(ModelStateConstant.EMAIL_REGEX, ErrorMessage = ModelStateConstant.EMAIL_ERROR_MESSAGE)]
    public string Email { get; set; } = null!;

    [Required(ErrorMessage = ModelStateConstant.EMPTY_PASSWORD_ERROR_MESSAGE)]
    public string Password { get; set; } = null!;
}