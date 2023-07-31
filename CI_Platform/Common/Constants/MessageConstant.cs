
namespace Common.Constants;

public class MessageType
{
    public const string EXCEPTION = "Exception";

    public const string INFORMATION = "Information";
}

public static class ExceptionMessage
{
    public const string UNAUTHORIZED = "Unauthorized to access this resource";

    public const string TOKEN_EXPIRED = "Session is Expired, try again!";

    public const string RESET_PASSWORD_TOKEN_EXPIRED = "Change password link is expired";

    public const string INTERNAL_SERVER = "An error occurred while processing the request";

    public const string CONCURRENCY = "Try after some time!";

    public const string ENTITY_UPDATE = "Duplicate or malicious record!";

    public const string DEFAULT_MODELSTATE = "Model state is invalid!";

    public const string UNAUTHENTICATED = "Email id or password is incorrect!";

    public const string FILE_IS_NULL = "File is null or empty!";

    public const string ID_IS_NULL_OR_ZERO = "Id cannot be null or 0";

    public const string ADMIN_NOT_FOUND = "Admin Not Found!";

    public const string MAIL_NOT_SENT = "Please try again! mail not sent";

    public const string INVALID_CLAIMS = "Invalid claims";

    public const string ADMIN_ALREADY_EXIST = "Admin is already exist!";

    public const string SKILL_NOT_FOUND = "Skill Not Found!";

    public const string SKILL_ALREADY_EXIST = "Skill is already exist!";

    public const string COUNTRY_NOT_FOUND = "Country Not Found!";

    public const string COUNTRY_STATUS_REQUIRED = "Country Status is Required, Cannot be null or 0";

    public const string GREATER_THAN_ZERO = "Value should be greater than 0";

    public const string INVALID_SORCOLUMN = "Invalid Column name for sorting!";

    #region Volunteer
    public const string VOLUNTEER_ALREADY_EXIST = "Volunteer is already exist!";

    public const string EMAIL_ALREADY_REGISTERED = "Email id already registred";

    public const string VOLUNTEER_NOT_FOUND = "Volunteer Not Found!";
    #endregion

    public const string CITY_NOT_FOUND = "City Not Found!";

    public const string MISSION_THEME_NOT_FOUND = "Mission Theme Not Found!";

    public const string MISSION_NOT_FOUND = "Mission Not Found!";

    public const string COUNTRY_ALREADY_EXIST = "Country already exist!";

    public const string CITY_ALREADY_EXIST = "City already exist!";

    public const string MISSION_THEME_ALREADY_EXIST = "Mission Theme already exist!";

    public const string COUNTRY_IN_USE = "Country is in use!";

    public const string AVATAR_NOT_VALID = "Invalid Avatar image format!";


    #region Banner
    public const string SORT_ORDER_ALREADY_EXIST = "Sort order already exist!";

    public const string BANNER_ALREADY_EXIST = "Banner already exist!";

    public const string BANNER_NOT_FOUND = "Banner not found!";

    public const string SAME_BANNER_ORDER = "Banner orders are same!";

    #endregion

    public const string CMS_PAGE_ALREADY_EXIST = "CMS Page already exist!";

    public const string CMS_PAGE_NOT_FOUND = "CMS Page Not Found";

    public const string CITY_NOT_ACTIVE = "Given City is either deleted or not active!";

    public const string MISSION_THEME_NOT_ACTIVE = "Given Mission Theme is either deleted or not active!";

    public const string MISSION_SKILLS_NOT_ACTIVE = "From given Mission Skills any skill is either deleted or not active!";
    public const string CITY_IN_USE_FOR_MISSION = "City is in use for Mission!";

    public const string CITY_IN_USE_BY_VOLUNTEER = "City is in use by Volunteer!";

    public const string MISSION_THEME_IN_USE_FOR_MISSION = "Mission Theme is already in use for Mission!";

    #region Mission Application
    public const string ALREADY_APPLIED_FOR_MISSION = "Already applied!";
    public const string ALREADY_GOAL_VALUE_ACHIEVED = "Goal value achieved already!";
    public const string EARLY_APPLY_MISSION_ERROR = "Mission has not started yet!";
    public const string LATE_APPLY_MISSION_ERROR = "Mission deadline is gone!";
    public const string SEAT_FULL_MISSION_ERROR = "No seats are available!";
    public const string MISSION_APPLICATION_NOT_FOUND = "Mission application not found!";
    public const string MISSION_APPLICATION_STATUS_ERROR = "Status type required while updating application!";
    public const string MISSION_APPLICATION_NOT_APPLIED = "Not applied!";
    #endregion
    public const string INVALID_RATING = "Invalid Rating!";

    public const string RATING_NOT_FOUND = "Rating not Found!";

    public const string FAVOURITES_NOT_FOUND = "Favourites not Found!";

    public const string CANNOT_POST_EMPTY_COMMENT = "Cannot Post Empty Comment!";
    
    #region Timesheet
    public const string TIMESHEET_NOT_FOUND = "Timesheet Not Found or Deleted!";

    public const string INVALID_OPRATION_TIMESHEET = "Timesheet data has already been either approved or declined. Editing is no longer permitted.";

    public const string INVALID_MISSION_TIMESHEET = "Invalid mission you selected!";

    public const string TIMESHEET_STARTDATE = "The volunteered date must be between the approved date of the mission application and today's date.";

    public const string TIMESHEET_ENDDATE = "The volunteered date must be before the Enddate.";

    public const string TIMESHEET_GOALVALUE = "The value of goals achieved exceeds the remaining goal value by #.";

    public const string TIMESHEET_STATUS_ERROR = "Status type required while updating timesheet!";
    #endregion
}

public static class SuccessMessage
{
    public const string LOGIN_SUCCESS = "Login successfully!";

    public const string LOGOUT_SUCCESS = "Logout successfully!";

    public const string REGISTERATION = "Welcome to CI platform";

    public const string ADMIN_REGISTERATION = "Registered as an Admin!";

    public const string ADMIN_UPDATED = "Admin updated successfully!";

    public const string ADMIN_DELETED = "Admin deleted successfully!";

    public const string FORGOT_PASSWORD_MAIL_SENT = "If you have an account with us, Mail is sent to verified account.";

    public const string PASSWORD_CHANGE_SUCCESS = "Password changed successfully!";

    #region Country
    public const string ADD_COUNTRY = "Country added successfully";

    public const string UPDATE_COUNTRY = "Country updated successfully";

    public const string DELETE_COUNTRY = "Country deleted successfully";
    #endregion

    #region Skill
    public const string ADD_SKILL = "Skill added successfully!";

    public const string UPDATE_SKILL = "Skill updated successfully!";

    public const string DELETE_SKILL = "Skill deleted successfully!";
    #endregion

    #region City
    public const string ADD_CITY = "City added successfully";

    public const string UPDATE_CITY = "City updated successfully";

    public const string DELETE_CITY = "City deleted successfully";
    #endregion

    #region Mission
    public const string ADD_MISSION = "Mission added successfully";

    public const string UPDATE_MISSION = "Mission updated successfully";

    public const string DELETE_MISSION = "Mission deleted successfully";
    #endregion

    #region Volunteer
    public const string REGISTRATION_VOLUNTEER = "Registered as a Volunteer!";

    public const string ADD_VOLUNTEER = "Volunteer added successfully";

    public const string UPDATE_VOLUNTEER = "Volunteer updated successfully";

    public const string UPDATE_VOLUNTEER_PROFILE = "Profile updated successfully";

    public const string UPDATE_AVATAR = "Profile Image updated successfully";

    public const string DELETE_VOLUNTEER = "Volunteer deleted successfully";
    #endregion

    #region MissionTheme

    public const string ADD_MISSION_THEME = "Mission Theme added successfully";

    public const string UPDATE_MISSION_THEME = "Mission Theme updated successfully";

    public const string DELETE_MISSION_THEME = "Mission Theme deleted successfully";

    #endregion

    #region CMSPage

    public const string ADD_CMS_PAGE = "CMS Page added successfully";

    public const string UPDATE_CMS_PAGE = "CMS Page updated successfully";

    public const string DELETE_CMS_PAGE = "CMS Page deleted successfully";

    #endregion

    #region Banner
    public const string ADD_BANNER = "Banner added successfully";

    public const string UPDATE_BANNER = "Banner updated successfully";

    public const string UPDATE_BANNER_SORT_ORDER = "Banner order replaced successfully";

    public const string DELETE_BANNER = "Banner deleted successfully";
    #endregion

    #region MissionRating

    public const string POST_RATING = "Rating posted successfully";

    #endregion

    #region Story
    public const string ADD_STORY_DRAFT = "Story saved into draft";
    public const string ADD_STORY_PENDING = "Story added successfully and pending at admin side";
    #endregion

    #region Mission Application
    public const string MISSION_APPLY_SUCCESS = "Applied for mission, approval pending at admin side";
    #endregion

    #region MissionRating

    public const string LIKED_MISSION = "Mission Liked Successfully";

    public const string UNLIKED_MISSION = "Mission Unliked Successfully";

    #endregion

    #region Timesheet
    public const string ADD_TIMESHEET = "Timesheet added successfully!";

    public const string UPDATE_TIMESHEET = "Timesheet updated successfully!";

    public const string DELETE_TIMESHEET = "Timesheet deleted successfully!";
    #endregion

    #region MissionComments

    public const string POST_COMMENT = "Comment Posted Successfully";

    #endregion
}