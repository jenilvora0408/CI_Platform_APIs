using Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.ExtAuthorization;

public class AdminPolicyAttribute : AuthorizeAttribute
{
    public AdminPolicyAttribute()
    {
        Policy = SystemConstant.ADMIN_POLICY;
    }
}