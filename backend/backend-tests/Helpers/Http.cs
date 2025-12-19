using backend.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.DependencyInjection;

namespace backend_tests.Helpers;

public static class Http
{
    // Validates an object using the ValidationFilter
    public static async Task<Dictionary<string, object?>> Validate(object obj)
    {
        FakeEndpointFilterContext filterCtx = new(obj);
        ValidationFilter filter = new();

        var resultObj = await filter.InvokeAsync(filterCtx, CreateNext);

        if (resultObj is not
            UnprocessableEntity<Dictionary<string, object?>>
            unprocessableEntityResult)
        {
            return new Dictionary<string, object?>();
        }

        HttpContext httpCtx = CreateContext();
        await unprocessableEntityResult.ExecuteAsync(httpCtx);
        return unprocessableEntityResult.Value ?? new Dictionary<string, object?>();
    }

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
    private static ValueTask<object?> CreateNext(EndpointFilterInvocationContext _)
    {
        return ValueTask.FromResult<object?>(TypedResults.Ok("NEXT"));
    }

}