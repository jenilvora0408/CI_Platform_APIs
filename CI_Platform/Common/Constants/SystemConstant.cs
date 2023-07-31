namespace Common.Constants;

public static class SystemConstant
{
    public const string CONNECTION_STRING_NAME = "DefaultConnection";

    public const string JWT_SETTING = "JwtSetting";

    public const string RESET_PASSWORD_JWT_SETTING = "ResetPasswordJwtSetting";

    public const string EMAIL_SETTING = "EmailSetting";

    public const string DEFAULT_AVATAR = "'/assets/avatar/profile.png'";

    public const string DEFAULT_DATETIME = "(getutcdate())";

    public const string WWWROOT_PATH = "/wwwroot";

    public const string ASSETS_PATH = "/assets/";

    public const string EMAIL_TEMPLATES_PATH = "/wwwroot/EmailTemplates/";

    public const string EMAIL_HEADING_RESET_PASSWORD = "Reset Password";

    public const string DIR_AVATAR = "avatar";

    public const string DIR_BANNER = "banner";

    public const string DIR_MISSION_IMAGE = "mission-images";

    public const string DIR_MISSION_DOC = "mission-documents";

    public const string ADMIN_POLICY = "AdminPolicy";

    public const string VOLUNTEER_POLICY = "VolunteerPolicy";

    public const string SESSION_USER = "SessionUser";

    public const string CACHE_KEY = "AuthTokenBlacklist";

    public const string PREFIX_BEARER_TOKEN = "Bearer ";

    public const int KEY_SIZE = 64;

    public const int ITERATION_COUNT = 3500000;

    public const string ASCENDING = "ascending";

    public const string DESCENDING = "descending";

    public const int DEFAULT_PAGE_SIZE = 10;

    public const string ENDPOINT_RESET_PASSWORD = "/Accounts/ResetPassword";

    public const string CORS_ALLOW_ANY = "AllowAny";

    public const string DEFAULT_SORTCOLUMN = "Id";

    public const string NAMEOF_SORTCOLUMN_SORTORDER = "SortOrder";

    public const int RESET_PASSWORD_TOKEN_EXPIRY_MINUTES = 5;

    public const string DIR_STORY_MEDIA = "story-media";

    public const string YOUTUBE_LINK_TYPE = "youtube";

    //TODO: Change
    public const string URL = "https://localhost:44302";

    public const int MIN_RATING = 0;

    public const int MAX_RATING = 5;

    public const int RECENT_VOLUNTEER_PAGE_SIZE = 9;

    public const string RECENT_VOLUNTEER_SORT_COLUMN = "ModifiedOn";
}
