using Common.Constants;
using System.ComponentModel.DataAnnotations;

namespace Entities.DTOs;

public class VolunteerRegistrationDTO
{
    [Required]
    [MaxLength(20, ErrorMessage = ModelStateConstant.FIRSTNAME_LENGTH_ERROR)]
    public string FirstName { get; set; } = null!;

    [Required]
    [MaxLength(20, ErrorMessage = ModelStateConstant.LASTNAME_LENGTH_ERROR)]
    public string LastName { get; set; } = null!;

    [Required]
    [RegularExpression(ModelStateConstant.REGEX_PHONE_NUMBER, ErrorMessage = ModelStateConstant.ERROR_PHONE_NUMBER)]
    public string PhoneNumber { get; set; } = null!;

    [Required]
    [MaxLength(120)]
    [RegularExpression(ModelStateConstant.EMAIL_REGEX, ErrorMessage = ModelStateConstant.EMAIL_ERROR_MESSAGE)]
    public string Email { get; set; } = null!;

    [Required]
    [StringLength(15, MinimumLength = 8, ErrorMessage = ModelStateConstant.PASSWORD_LENGTH_ERROR_MESSAGE)]
    [RegularExpression(ModelStateConstant.PASSWORD_POLICY, ErrorMessage = ModelStateConstant.PASSWORD_ERROR_MESSAGE)]
    public string Password { get; set; } = null!;

    [Required]
    [Compare(nameof(Password), ErrorMessage = ModelStateConstant.COMPARE_PASSWORD_ERROR_MESSAGE)]
    public string ConfirmPassword { get; set; } = null!;
}
