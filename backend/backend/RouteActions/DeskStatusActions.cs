using backend.Errors;
using backend.Models.DTO;
using backend.Services.DeskStatus;

namespace backend.RouteActions;

public static class DeskStatusActions
{
    public static async Task<IResult> GetAll(IDeskStatusService service)
    {
        return TypedResults.Ok(await service.FindAllAsync());
    }
    
    public static async Task<IResult> GetById(long id, IDeskStatusService service)
    {
        DeskStatusData res = await service.FindByIdAsync(id);

        if (res == DeskStatusData.Empty)
        {
           return ApiError.NotFound(id, "Entity of type DeskStatus was not found")
                          .ToResult();
        }
        return TypedResults.Ok(res);
    }
}