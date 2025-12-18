using backend.Models.DTO;
using backend.RouteActions;
using backend.Services.Desk;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace backend_tests;

public class DeskTests
{
    [Fact]
    public async Task DeskActionsGetAll_WithEmptyList_ReturnsOk()
    {
        var mock = new Mock<IDeskService>();
        mock.Setup(s => s.GetAllAsync(CancellationToken.None))
            .ReturnsAsync([]);
        
        var result = await DeskActions.GetAllAsync(mock.Object);
        
        Assert.True(
            result is Ok<List<DeskData>> desks && 
            desks.Value!.Count == 0
            );
    }
}