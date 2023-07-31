using Common.Constants;
using Common.Utils.Models;

namespace API.Extension;

public static class HttpContextExtension
{
    public static SessionUserModel GetSessionUser(this HttpContext httpContext)
    {
        return httpContext.Items[SystemConstant.SESSION_USER] as SessionUserModel ?? throw new UnauthorizedAccessException();
    }

    public static bool IsSessionUserExist(this HttpContext httpContext)
    {
        SessionUserModel? model = httpContext.Items[SystemConstant.SESSION_USER] as SessionUserModel;
        return model != null;
    }

    public static string GetAuthToken(this HttpContext httpContext)
    {
        var authorizationHeader = httpContext.Request.Headers.Authorization.FirstOrDefault();

        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.ToString().StartsWith(SystemConstant.PREFIX_BEARER_TOKEN))
        {
            throw new UnauthorizedAccessException(ExceptionMessage.UNAUTHORIZED);
        }

        string authToken = authorizationHeader.ToString().Replace(SystemConstant.PREFIX_BEARER_TOKEN, string.Empty);
        return authToken;
    }
}