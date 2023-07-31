using Common.Constants;
using Common.CustomExceptions;
using Entities.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Entities.CustomValidator;
public class CustomUserValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null) throw new EntityNullException("User can not be null");

        UserDTO dto = (UserDTO)validationContext.ObjectInstance;
        List<string> validationErrors = new();

        if (dto.Id == 0)
        {
            if (string.IsNullOrEmpty(dto.Email))
            {
                validationErrors.Add(ModelStateConstant.EMPTY_EMAIL_ERROR_MESSAGE);
            }
            if (string.IsNullOrEmpty(dto.Password))
            {
                validationErrors.Add(ModelStateConstant.EMPTY_PASSWORD_ERROR_MESSAGE);
            }
            if (string.IsNullOrEmpty(dto.ConfirmPassword))
            {
                validationErrors.Add(ModelStateConstant.COMPARE_PASSWORD_ERROR_MESSAGE);
            }

            if(validationErrors.Any())
            {
                throw new Common.CustomExceptions.ValidationException(validationErrors);
            }
        }

        return base.IsValid(value, validationContext);
    }
}
