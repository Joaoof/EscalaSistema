namespace EscalaSistema.API.Policy;

using EscalaSistema.API.Domain.Errors;
using Microsoft.AspNetCore.Authorization;

public class CanPublishScaleHandler : AuthorizationHandler<CanPublishScaleRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CanPublishScaleRequirement requirement)
    {
        if (!context.User.Identity?.IsAuthenticated ?? true)
            throw new DomainException(AuthErrors.NotAuthenticated);

        if (!context.User.IsInRole("Leader"))
            throw new DomainException(AuthErrors.NotAllowedToPublishScale);

        if (!context.User.IsInRole("Leader"))
            throw new DomainException(AuthErrors.NotAllowedToModifyScale);

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
