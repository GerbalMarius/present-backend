using System.ComponentModel.DataAnnotations;
using backend.Errors;

namespace backend.Filters;

public sealed class ValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        Dictionary<string, string> errors = [];

        foreach (var contextArgument in context.Arguments)
        {
            if (contextArgument is null) continue;

            var type = contextArgument.GetType();
            if (type.IsPrimitive || contextArgument is string) continue;
            List<ValidationResult> validationResults = [];

            ValidationContext vc = new(contextArgument);

            PopulateValidationErrors(errors, contextArgument, vc, validationResults);
        }

        if (errors.Count > 0)
        {
            return ApiError
                .UnprocessableEntity(errors)
                .ToResult(); 
        }

        return await next(context);
    }

    private static void PopulateValidationErrors(
        Dictionary<string, string> errors,
        object arg,
        ValidationContext validationContext,
        List<ValidationResult> validationResults)
    {
        if (Validator.TryValidateObject(arg, validationContext, validationResults, true)) return;

        foreach (var vr in validationResults)
        {
            var key = vr.MemberNames.FirstOrDefault() ?? "";
            var message = vr.ErrorMessage ?? "Invalid value";

            if (string.IsNullOrWhiteSpace(key)) continue;

            errors.TryAdd(key, message);
        }
    }
}