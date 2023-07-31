using Microsoft.AspNetCore.Mvc.Filters;
using Services.Interfaces;

namespace API.ExtAuthorization;

/// <summary>
/// Custom authorization filter that checks for the presence of a valid Bearer token in the Authorization header.
/// </summary>
public class ExtAuthorizeFilter : IAuthorizationFilter
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ITokenBlacklistService _tokenBlacklistService;


    public ExtAuthorizeFilter(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITokenBlacklistService tokenBlacklistService)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _tokenBlacklistService= tokenBlacklistService;
    }

    /// <summary>
    /// Performs authorization checks based on the presence of a valid Bearer token in the Authorization header.
    /// </summary>
    /// <param name="context">The authorization filter context.</param>
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        if (_httpContextAccessor == null || _httpContextAccessor.HttpContext == null)
            throw new UnauthorizedAccessException();

        new AuthHelper(_tokenBlacklistService).AuthorizeRequest(_httpContextAccessor.HttpContext, _configuration);
    }
}