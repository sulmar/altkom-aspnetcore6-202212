using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Altkom.Shopper.Api.Authorization;

public class MinimumAgeAuthorizationHandler : AuthorizationHandler<MinimumAgeRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        MinimumAgeRequirement requirement)
    {        
        var dateOfBirth = DateTime.Parse(context.User.FindFirstValue(ClaimTypes.DateOfBirth));

        var age = DateTime.Now.Year - dateOfBirth.Year;

        if (age >= requirement.MinimumAge)
        {
            context.Succeed(requirement);
        }
        else
            context.Fail();

        return Task.CompletedTask;
    }
}
