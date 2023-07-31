using Common.Constants;
using Entities.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Entities.CustomValidator
{
    internal class CustomStoryValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            VolunteerStoryDTO story = (VolunteerStoryDTO)validationContext.ObjectInstance;
            List<string> validationErrors = new List<string>();

            if (story.Status != null && (story.Status != Common.Enums.StoryStatus.DRAFT && story.Status != Common.Enums.StoryStatus.PENDING))
            {
                validationErrors.Add(ModelStateConstant.STATUS_INVALID);
            }

            // Check if Medias or MediaUrls is provided
            if (story.Medias == null && (story.MediaUrls == null || story.MediaUrls.Count == 0))
            {
                validationErrors.Add(ModelStateConstant.MEDIA_ERROR);
            }

            if (validationErrors.Count > 0)
            {
                throw new Common.CustomExceptions.ValidationException(validationErrors);
            }

            return ValidationResult.Success;
        }
    }
}