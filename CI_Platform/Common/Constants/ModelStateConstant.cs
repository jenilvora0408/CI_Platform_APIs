namespace Common.Constants;
public static class ModelStateConstant
{
    public const string EMAIL_REGEX = @"[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[A-Za-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[A-Za-z0-9](?:[a-zA-Z0-9-]*[A-Za-z0-9])?\.)+[A-Za-z0-9](?:[A-Za-z0-9-]*[A-Za-z0-9])?";

    public const string EMAIL_ERROR_MESSAGE = "Not Valid Email Address";

    public const string EMPTY_EMAIL_ERROR_MESSAGE = "Email is Required";

    public const string PASSWORD_POLICY = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()_+\\-=[\\]{};':\"\\\\|,.<>/?]).+$";

    public const string EMPTY_PASSWORD_ERROR_MESSAGE = "Password is Required";

    public const string PASSWORD_LENGTH_ERROR_MESSAGE = "The password must be between 8 and 15 characters long.";

    public const string PASSWORD_ERROR_MESSAGE = "The password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character.";

    public const string COMPARE_PASSWORD_ERROR_MESSAGE = "Password and Confirm Password Not Match";

    public const string STATUS_INVALID = "Status Field Required or Invalid Status!";

    public const string VALIDATE_SORTORDER = "Sort Order must be ascending or descending!";

    public const string SORTORDER_REGEX = $"^({SystemConstant.ASCENDING}|{SystemConstant.DESCENDING})$";

    public const string FIRSTNAME_LENGTH_ERROR = "First name should not exceed 20 characters";

    public const string LASTNAME_LENGTH_ERROR = "Last name should not exceed 20 characters";

    public const string SKILLNAME_LENGTH_ERROR = "Skill name should not exceed 128 characters";

    public const string INVALID_CITY = "Invalid City!";

    public const string INVALID_COUNTRY = "Invalid Country!";

    public const string INVALID_MISSION_THEME = "Invalid Mission Theme!";

    public const string ONLY_ALPHABETS_ALLOWED = "Only alphabets are allowed!";

    #region Mission
    public const string MISSION_TITLE_LENGTH = "Mission Title should not exceed 255 characters";

    public const string SHORT_DESC_LENGTH = "Short Description should not exceed 1024 characters";

    public const string GOAL_OBJECTIVE_TITLE = "Goal Objective title should not exceed 128 characters";

    public const string MISSION_TYPE = "Mission Type Field Required or Invalid!";

    public const string AVAILABILTY = "Availability Field Required or Invalid!";

    public const string END_DATE = "End date is required for Mission Type Time!";

    public const string END_DATE_NOT_VALID = "End date must be after start date for Mission Type Time!";

    public const string REGI_DEADLINE_DATE_NOT_VALID = "Registration Date must be before end date!";

    public const string TOTAL_SEAT = "Total Seat is required for Mission Type Time!";

    public const string TOTAL_SEAT_NOT_VALID = "Total Seat must be greter than zero!";

    public const string GOAL_OBJECTIVE = "Goal objective value is required for Mission Type Goal!";

    public const string GOAL_OBJECTIVE_NOT_VALID = "Goal objective must be greater than zero!";

    public const string GOAL_OBJECTIVE_TITLE_REQUIRED = "Goal objective title is required for Mission Type Goal!";

    public const string MISSION_SKILLS_REPEAT = "Given Mission Skills Repeated!";

    public const string THUMBNAIL_URL = "Not Valid Thumbnail Image!";
    #endregion

    #region Volunteer
    public const string REGEX_PHONE_NUMBER = @"^[1-9]\d{9,13}$";

    public const string ERROR_PHONE_NUMBER = "Invalid Phone number.";

    public const string ERROR_CITY = "Invalid City";

    public const string AVAILABILITY = "Invalid Availability";

    public const string REASON_TO_BE_VOLUNTEER = "Reason to be a volunteer is not exceed 4000 character";
    #endregion

    #region Banner
    public const string ERROR_BANNER_DESCRIPTION = "Description should be atleast 50 characters long!";
    public const string ERROR_BANNER_SORT_ORDER = "Sort error should be given and unique!";
    public const string ERROR_BANNER_IMAGE = "Image is required for banner!";
    #endregion

    #region Story
    public const string STORY_TITLE_LENGTH = "Mission Title should not exceed 255 characters";
    public const string MEDIA_ERROR = "At least one file or valid URL should be provided.";
    #endregion

    #region Mission Application
    public const string INVALID_MISSION_ID = "Mission id is not valid!";
    #endregion

    #region Timesheet
    public const string TIMESHEET_NOTES = "Timesheet Notes should not exceed 1024 characters";

    public const string TIMESHEET_MISSIONID = "Mission Id must be greater than 0.";

    public const string TIMESHEET_VOLUNTEERID = "Volunteer Id must be greater than 0.";

    public const string TIMESHEET_GOALACHIEVED = "Goal achieved must be greater than 0.";

    public const string TIMESHEET_HOUR = "Hour must be greater than 0 and less than 24.";

    public const string TIMESHEET_MINUTE = "Minute must be greater than 0 and less than 60.";
    #endregion
}
