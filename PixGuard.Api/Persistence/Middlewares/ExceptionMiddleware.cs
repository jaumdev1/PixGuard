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
             
                case InvalidEnumValueException enumException:  
                    HandleEnumException(context, enumException);
                    break;
                
                case UnauthorizedAccessException unauthorizedAccessException:
                    HandleUnauthorizedException(context, unauthorizedAccessException);
                    break;
                
                case UserExistsException userExistsException:
                    HandleUserExistsException(context, userExistsException);
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
    private async void HandleUnauthorizedException(HttpContext context, UnauthorizedAccessException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 401; 
        var errorDetails = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Unauthorized access, incorrect login or password."
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
    }
    
    private async void HandleUserExistsException(HttpContext context, UserExistsException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 409; 
        var errorDetails = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "The email provided is already in use."
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
    }
    private async void HandleEnumException(HttpContext context, InvalidEnumValueException ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = 500; 
        var errorDetails = new
        {
            StatusCode = context.Response.StatusCode,
            Message = "Enum Convert Type Server Error"
        };
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
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