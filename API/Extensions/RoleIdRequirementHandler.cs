using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

public class RoleIdRequirementHandler : AuthorizationHandler<RoleIdRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RoleIdRequirement requirement)
    {
        var roleClaim = context.User.FindFirst(c => c.Type == "RoleId");
        if (roleClaim != null && int.TryParse(roleClaim.Value, out int userRoleId))
        {
            if (userRoleId == requirement.RequiredRoleId)
            {
                context.Succeed(requirement);
            }
        }

        return Task.CompletedTask;
    }


    //protected override Task HandleRequirementAsync(
    //    AuthorizationHandlerContext context,
    //    RoleIdRequirement requirement)
    //{
    //    var roleClaim = context.User.FindFirst(c => c.Type == "RoleId");
    //    if (roleClaim != null && int.TryParse(roleClaim.Value, out int userRoleId))
    //    {
    //        if (userRoleId == requirement.RequiredRoleId)
    //        {
    //            context.Succeed(requirement);
    //        }
    //    }

    //    return Task.CompletedTask;
    //}
}