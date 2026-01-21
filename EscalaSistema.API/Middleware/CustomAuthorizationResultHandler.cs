using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using System.Text.Json;

namespace EscalaSistema.API.Middleware;

public class CustomAuthorizationResultHandler : IAuthorizationMiddlewareResultHandler
{
    private readonly AuthorizationMiddlewareResultHandler _defaultHandler = new();

    public async Task HandleAsync(
        RequestDelegate next,
        HttpContext context,
        AuthorizationPolicy policy,
        PolicyAuthorizationResult authorizeResult)
    {
        if (authorizeResult.Forbidden)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            context.Response.ContentType = "application/json";

            var policyName = policy?.Requirements
                .FirstOrDefault()?.GetType().Name ?? "UnknownPolicy";

            var response = new
            {
                success = false,
                error = new
                {
                    code = "AUTH_403_SCALE_PUBLISH",
                    type = "AUTHORIZATION_FAILED",
                    message = "Você não possui permissão para publicar esta escala.",
                    policy = policyName
                }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
            return;
        }


        if (authorizeResult.Challenged)
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await _defaultHandler.HandleAsync(next, context, policy, authorizeResult);
    }
}