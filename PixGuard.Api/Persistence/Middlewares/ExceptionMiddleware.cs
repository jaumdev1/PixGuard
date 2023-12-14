using System.Text.Json;
using System.Text.Json.Serialization;
using Domain.Exceptions;

namespace PixGuard.Api.Persistence.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
           
            switch (ex)
            {
                case PixException customException:
                    HandlePixException(context, customException);
                    break;
             
                default:
                    HandleGenericException(context, ex);
                    break;
            }
        }
    }

    private void HandlePixException(HttpContext context, PixException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 400; 
    }
    
    private async void HandleGenericException(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500;
        var errorDetails = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Internal Server Error"
        };

        await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
    }
}