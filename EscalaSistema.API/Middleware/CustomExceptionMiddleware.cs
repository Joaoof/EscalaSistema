using EscalaSistema.API.Domain.Errors;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EscalaSistema.API.Middleware;

public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (DomainException ex)
        {
            var problem = new ProblemDetails
            {
                Title = ex.Error.Title,
                Detail = ex.Error.Detail,
                Status = ex.Error.StatusCode,
                Type = $"https://httpstatuses.com/{ex.Error.StatusCode}"
            };

            problem.Extensions["code"] = ex.Error.Code;

            context.Response.StatusCode = ex.Error.StatusCode;
            context.Response.ContentType = "application/problem+json";

            await context.Response.WriteAsJsonAsync(problem);
        }

        catch (Exception ex)
        {
            // 2. LOGUE O ERRO REAL AQUI
            _logger.LogError(ex, "Erro inesperado aconteceu: {Message}", ex.Message);

            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            // (Opcional para Debug) Você pode passar ex.Message temporariamente
            // mas em produção, mantenha a mensagem genérica.
            var response = new
            {
                success = false,
                error = new
                {
                    code = "INTERNAL_ERROR",
                    message = "Erro interno no servidor" // O erro real agora está no console/logs
                }
            };

            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
