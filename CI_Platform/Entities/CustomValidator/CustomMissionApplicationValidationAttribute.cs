using Common.Constants;
using Entities.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Entities.CustomValidator
{
    internal class CustomMissionApplicationValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            VolunteerMissionApplicationDTO obj = (VolunteerMissionApplicationDTO)validationContext.ObjectInstance;
            List<string> validationErrors = new List<string>();

            if (obj.MissionId <= 0)
            {
                validationErrors.Add(ModelStateConstant.INVALID_MISSION_ID);
            }

            if(validationErrors.Any())
            {
                throw new Common.CustomExceptions.ValidationException(validationErrors);
            }

            return ValidationResult.Success;
        }
    }
}