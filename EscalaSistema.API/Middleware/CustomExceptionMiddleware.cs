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

        int statusCode = exception switch
        {
            BadRequestException br => br.StatusCode,
            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        await context.Response.WriteAsync(JsonSerializer.Serialize(new
        {
            success = false,
            message = exception.Message
        }));
    }

}
