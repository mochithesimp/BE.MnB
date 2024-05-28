using Microsoft.AspNetCore.Authorization;

public class RoleIdRequirement : IAuthorizationRequirement
{
    public int RequiredRoleId { get; }

    public RoleIdRequirement(int requiredRoleId)
    {
        RequiredRoleId = requiredRoleId;
    }
}
