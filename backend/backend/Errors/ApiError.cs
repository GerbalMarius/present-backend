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
    
    public static ApiError UnprocessableEntity(Dictionary<string, string> errors) => Init()
        .HttpStatus(StatusCodes.Status422UnprocessableEntity)
        .Code("UNPROCESSABLE_ENTITY")
        .With("errors", errors)
        .Message("There were validation errors");
    
    public static ApiError BadRequest(string? msg) => Init()
        .HttpStatus(StatusCodes.Status400BadRequest)
        .Code("BAD_REQUEST")
        .Message(msg ?? "Bad request");
    

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
    public Dictionary<string, object?> ToBody()
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
        Dictionary<string, object?> body = ToBody();
        
        return _httpStatus switch
        {
            StatusCodes.Status404NotFound => TypedResults.NotFound(body),
            StatusCodes.Status400BadRequest => TypedResults.BadRequest(body),
            StatusCodes.Status409Conflict => TypedResults.Conflict(body),
            StatusCodes.Status422UnprocessableEntity => TypedResults.UnprocessableEntity(body),
            _ => TypedResults.Json(body, contentType: "application/json", statusCode: _httpStatus)
        };
    }
}