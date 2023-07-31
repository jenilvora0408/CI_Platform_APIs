using Common.Constants;
using Common.Enums;
using Entities.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Entities.CustomValidator;

public class CustomMissionValidatorAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        MissionFormDTO mission = (MissionFormDTO)validationContext.ObjectInstance;
        List<string> validationErrors = new();

        if (mission.MissionType == MissionType.Time)
        {
            if (mission.EndDate == null)
                validationErrors.Add(ModelStateConstant.END_DATE);

            if (string.IsNullOrEmpty(mission.TotalSeat.ToString()) || mission.TotalSeat is string)
                validationErrors.Add(ModelStateConstant.TOTAL_SEAT);
        }

        if (mission.TotalSeat != null && mission.TotalSeat <= 0)
            validationErrors.Add(ModelStateConstant.TOTAL_SEAT_NOT_VALID);

        if (mission.EndDate != null && mission.EndDate < mission.StartDate)
            validationErrors.Add(ModelStateConstant.END_DATE_NOT_VALID);

        if (mission.RegistrationDeadline != null && mission.RegistrationDeadline >= mission.EndDate)
            validationErrors.Add(ModelStateConstant.REGI_DEADLINE_DATE_NOT_VALID);

        if (mission.MissionType == MissionType.Goal)
        {
            if (string.IsNullOrEmpty(mission.GoalObjectiveTitle))
                validationErrors.Add(ModelStateConstant.GOAL_OBJECTIVE_TITLE_REQUIRED);

            if (mission.GoalObjective == null || mission.GoalObjective == 0)
                validationErrors.Add(ModelStateConstant.GOAL_OBJECTIVE);
        }

        if (!string.IsNullOrEmpty(mission.GoalObjective.ToString()) && mission.GoalObjective <= 0)
            validationErrors.Add(ModelStateConstant.GOAL_OBJECTIVE_NOT_VALID);

        if (string.IsNullOrEmpty(mission.CityId.ToString()) || mission.CityId is string || mission.CityId <= 0)
        {
            validationErrors.Add(ModelStateConstant.INVALID_CITY);
        }

        if (string.IsNullOrEmpty(mission.MissionThemeId.ToString()) || mission.MissionThemeId is string || mission.MissionThemeId <= 0)
        {
            validationErrors.Add(ModelStateConstant.INVALID_MISSION_THEME);
        }

        if (mission.MissionSkills.Count() > 0 && mission.MissionSkills.Count() != mission.MissionSkills.Distinct().Count())
            validationErrors.Add(ModelStateConstant.MISSION_SKILLS_REPEAT);

        if (!mission.ThumbnailUrl.ContentType.Contains("image"))
            validationErrors.Add(ModelStateConstant.THUMBNAIL_URL);


        if (validationErrors.Count > 0)
        {
            throw new Common.CustomExceptions.ValidationException(validationErrors);
        }

        return ValidationResult.Success;
    }
}
