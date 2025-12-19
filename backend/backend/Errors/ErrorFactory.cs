using backend.Errors.Exceptions;

namespace backend.Errors;

//used to quickly create error response bodies after an unhandled exception happened
public static class ErrorFactory
{
    public static Dictionary<string, object?> NotFound(NotFoundException nfe)
    {
        return ApiError
            .NotFound(nfe.Id, nfe.Message)
            .ToBody();
    }

    public static Dictionary<string, object?> Unexpected(Exception ex)
    {
        return ApiError.Init()
            .Message(ex.Message)
            .With("cause", ex.GetType().FullName)
            .ToBody();
    }

    public static Dictionary<string, object?> BadRequest(BadHttpRequestException bhre)
    {
        return ApiError.BadRequest(bhre.Message)
            .ToBody();
    }
}