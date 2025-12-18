using backend.Errors;
using backend.Models.DTO;
using backend.Services.Desk;

namespace backend.RouteActions;

public static class DeskActions
{
    public static async Task<IResult> GetAllAsync(IDeskService service)
    {
        return TypedResults.Ok(await service.GetAllAsync());
    }

    public static async Task<IResult> GetByIdAsync(long id, IDeskService service)
    {
        DeskData result = await service.GetByIdAsync(id);

        if (result == DeskData.Empty)
        {
            return ApiError.NotFound(id, "Desk not found")
                           .ToResult();
        }
        return TypedResults.Ok(result);
    }
}