using backend.Errors.Exceptions;

namespace backend.Errors;

public static class ErrorFactory
{
    public static Dictionary<string, object?> NotFound(NotFoundException ex)
    {
        return ApiError
            .NotFound(ex.Id, ex.Message)
            .Build();
    }

    public static Dictionary<string, object?> Unexpected(Exception ex)
    {
        return ApiError.Init()
            .Message(ex.Message)
            .With("cause", ex.GetType().FullName)
            .Build();
    }
}