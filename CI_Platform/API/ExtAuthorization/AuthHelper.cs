using Common.Constants;
using Common.Utils.Models;
using Common.Utils;
using System.Security.Claims;
using API.Extension;
using Services.Interfaces;

namespace API.ExtAuthorization;


public class AuthHelper
{
    private readonly ITokenBlacklistService _tokenBlacklistService;

    public AuthHelper(ITokenBlacklistService tokenBlacklistService)
    {
        _tokenBlacklistService = tokenBlacklistService;
    }

    internal void AuthorizeRequest(HttpContext httpContext, IConfiguration configuration)
    {
        string authToken = httpContext.GetAuthToken();

        //Check if token is revoked
        bool isTokenBlacklisted = _tokenBlacklistService.IsTokenBlacklisted(authToken);

        if (isTokenBlacklisted)
        {
            throw new UnauthorizedAccessException(ExceptionMessage.TOKEN_EXPIRED);
        }

        JwtSetting jwtSetting = GetJwtSetting(configuration);

        ClaimsPrincipal? claims = JwtHelper.ValidateJwtToken(jwtSetting, authToken);

        if (claims == null)
        {
            throw new UnauthorizedAccessException();
        }

        // Create the CurrentUserModel object from the claims
        SetSessionUser(httpContext, claims);
    }

    private static JwtSetting GetJwtSetting(IConfiguration configuration)
    {
        JwtSetting jwtSetting = new();
        configuration.GetSection(SystemConstant.JWT_SETTING).Bind(jwtSetting);
        return jwtSetting;
    }

    private static void SetSessionUser(HttpContext httpContext, ClaimsPrincipal claims)
    {
        var sessionUser = new SessionUserModel
        {
            Id = long.Parse(claims.FindFirstValue(ClaimTypes.Name)),
            Name = claims.FindFirstValue(ClaimTypes.NameIdentifier),
            Email = claims.FindFirstValue(ClaimTypes.Email),
            Type = int.Parse(claims.FindFirstValue(ClaimTypes.Role))
        };

        // Set the authenticated user
        var identity = new ClaimsIdentity(claims.Identity);
        var principal = new ClaimsPrincipal(identity);
        httpContext.User = principal;

        // Attach the CurrentUserModel to the HttpContext.User
        httpContext.Items[SystemConstant.SESSION_USER] = sessionUser;
    }
}
