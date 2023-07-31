using Common.Constants;
using Common.CustomExceptions;
using Common.Enums;
using Common.Utils.Models;
using Microsoft.AspNetCore.Authorization;
using Services.Interfaces;

namespace API.ExtAuthorization;

public class ExtAuthorizeHandler : AuthorizationHandler<ExtAuthorizeRequirement>
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _configuration;
    private readonly ITokenBlacklistService _tokenBlacklistService;

    public ExtAuthorizeHandler(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, ITokenBlacklistService tokenBlacklistService)
    {
        _httpContextAccessor = httpContextAccessor;
        _configuration = configuration;
        _tokenBlacklistService = tokenBlacklistService;
    }

    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ExtAuthorizeRequirement requirement)
    {
        HttpContext? httpContext = _httpContextAccessor.HttpContext;

        if (httpContext == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        new AuthHelper(_tokenBlacklistService).AuthorizeRequest(httpContext, _configuration);


        if (CheckUserType(httpContext, requirement))
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        context.Fail();
        return Task.CompletedTask;
    }

    private static bool CheckUserType(HttpContext context, ExtAuthorizeRequirement requirement)
    {
        SessionUserModel? sessionUser = context.Items[SystemConstant.SESSION_USER] as SessionUserModel;

        if (sessionUser == null)
        {
            return false;
        }

        // Handle the AdminPolicy requirement
        if (requirement.PolicyName == SystemConstant.ADMIN_POLICY)
        {
            if (sessionUser.Type == (int)UserType.Admin)
            {
                return true;
            }
        }
        else if (requirement.PolicyName == SystemConstant.VOLUNTEER_POLICY)
        {
            if (sessionUser.Type == (int)UserType.Volunteer)
            {
                return true;
            }
        }

        throw new ForbiddenException(ExceptionMessage.UNAUTHORIZED);
    }
}