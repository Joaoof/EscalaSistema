using System.Net;
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
        catch (Exception ex)
        {
           await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        _logger.LogError(exception, exception.Message);

        context.Response.ContentType = "application/json";

        // 1️⃣ FluentValidation
        if (exception is FluentValidation.ValidationException validationEx)
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;

            var errors = validationEx.Errors
                .Select(e => e.ErrorMessage)
                .ToList();

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                success = false,
                message = "Erro de validação",
                errors = errors
            }));

            return;
        }

        // 2️⃣ BadRequest custom
        if (exception is BadRequestException badRequest)
        {
            context.Response.StatusCode = badRequest.StatusCode;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                success = false,
                message = badRequest.Message
            }));

            return;
        }

        // 3️⃣ NotFound custom (se existir)
        if (exception is NotFoundException notFound)
        {
            context.Response.StatusCode = StatusCodes.Status404NotFound;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new
            {
                success = false,
                message = notFound.Message
            }));

            return;
        }

        // 4️⃣ Fallback
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            success = false,
            message = "Erro interno no servidor"
        }));
    }
}
