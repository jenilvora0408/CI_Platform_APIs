using Common.Constants;
using Microsoft.AspNetCore.Authorization;

namespace API.ExtAuthorization;

public class VolunteerPolicyAttribute : AuthorizeAttribute
{
    public VolunteerPolicyAttribute()
    {
        Policy = SystemConstant.VOLUNTEER_POLICY;
    }
}