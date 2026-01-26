namespace EscalaSistema.API.Policy;

using EscalaSistema.API.Domain.Errors;
using Microsoft.AspNetCore.Authorization;

public class CanPublishScaleHandler : AuthorizationHandler<CanPublishScaleRequirement>
{
    protected override Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        CanPublishScaleRequirement requirement)
    {
        // 1. Não autenticado → 401
        if (!(context.User.Identity?.IsAuthenticated ?? false))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        // 2. Não autorizado → 403
        if (!context.User.IsInRole("Leader"))
        {
            context.Fail();
            return Task.CompletedTask;
        }

        // 3. Autorizado
        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
