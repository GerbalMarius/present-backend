using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace backend_tests.Helpers;

public static class Http
{
    // Creates a fake HttpContext
    public static HttpContext CreateContext()
    {
        var services = new ServiceCollection();

        services.AddLogging();
        services.AddOptions();

        services.AddProblemDetails();

        var sp = services.BuildServiceProvider();

        var ctx = new DefaultHttpContext
        {
            RequestServices = sp
        };

        return ctx;
    }
    
    // Creates a fake EndpointFilterInvocationContext for advancing to the next chain filter
    public static ValueTask<object?> CreateNext(EndpointFilterInvocationContext _)
    {
        return ValueTask.FromResult<object?>(TypedResults.Ok("NEXT"));
    }
}