using backend.Errors.Exceptions;

namespace backend.Errors;

public static class ErrorFactory
{
    public static Dictionary<string, object?> NotFound(NotFoundException nfe)
    {
        return ApiError
            .NotFound(nfe.Id, nfe.Message)
            .Build();
    }

    public static Dictionary<string, object?> Unexpected(Exception ex)
    {
        return ApiError.Init()
            .Message(ex.Message)
            .With("cause", ex.GetType().FullName)
            .Build();
    }

    public static Dictionary<string, object?> BadRequest(BadHttpRequestException bhre)
    {
        return ApiError.BadRequest(bhre.Message)
            .Build();
    }
}