using backend.Errors.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace backend.Errors;

public static class ErrorConfigurator
{
    public static async Task ConfigureResponseErrors(HttpContext httpContext)
    {
        Exception? ex = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        
        Dictionary<string, object?> body = 
            CreateBodyForException(ex ?? new Exception("Unexpected error occurred."));

        httpContext.Response.StatusCode = (int)(body["status"] ?? StatusCodes.Status500InternalServerError);
        httpContext.Response.ContentType = "application/json;charset=utf-8";

         await httpContext.Response.WriteAsJsonAsync(body);
    }

    private static Dictionary<string, object?> CreateBodyForException(Exception exception)
    {
        return exception switch
        {
            NotFoundException nfe => ErrorFactory.NotFound(nfe),
            BadHttpRequestException bhre => ErrorFactory.BadRequest(bhre),
            _ => ErrorFactory.Unexpected(exception)
        };
    }
}