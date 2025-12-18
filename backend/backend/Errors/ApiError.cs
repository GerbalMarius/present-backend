namespace backend.Errors;

public sealed class ApiError
{
    private int _httpStatus = StatusCodes.Status500InternalServerError;
    private string _code = "INTERNAL_SERVER_ERROR";
    private string _message = "Unexpected error occurred.";
    private readonly Dictionary<string, object?> _extras = new();

    private ApiError(){}
    
    public static ApiError Init() => new();
    
    public static ApiError NotFound(object? id, string? msg) => Init()
        .HttpStatus(StatusCodes.Status404NotFound)
        .Code("NOT_FOUND")
        .With("id", id)
        .Message(msg ?? "Entity was not found");

    public ApiError HttpStatus(int status)
    {
        _httpStatus = status;
        return this;
    }

    public ApiError Code(string code)
    {
        _code = code;
        return this;
    }

    public ApiError Message(string message)
    {
        _message = message;
        return this;
    }
    
    public ApiError With(string key, object? value)
    {
        _extras[key] = value;
        return this;
    }

    //for throwable exception handling
    public Dictionary<string, object?> Build()
    {
        Dictionary<string, object?> body = new()
        {
            ["status"] = _httpStatus,
            ["code"] = _code,
            ["message"] = _message
        };

        foreach (var kv in _extras)
        {
            body[kv.Key] = kv.Value;
        }

        return body;
    }
    
    //for returning the error as a plain response
    public IResult ToResult()
    {
        var body = Build();
        return _httpStatus switch
        {
            StatusCodes.Status404NotFound => TypedResults.NotFound(body),
            _ => TypedResults.Json(body, contentType: "application/json", statusCode: _httpStatus)
        };
    }
}