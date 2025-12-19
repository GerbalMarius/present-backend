using Microsoft.AspNetCore.Http;

namespace backend_tests.Helpers;

public class FakeEndpointFilterContext(params object?[] arguments) : EndpointFilterInvocationContext
{
    public override HttpContext HttpContext { get; } = Http.CreateContext();
    public override IList<object?> Arguments { get; } = arguments.ToList();
    
    public override T GetArgument<T>(int index)
    {
        return (T)Arguments[index]!;
    }

}