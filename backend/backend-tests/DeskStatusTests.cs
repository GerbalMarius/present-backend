using backend.Models.DTO;
using backend.RouteActions;
using backend.Services.DeskStatus;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace backend_tests;

public class DeskStatusTests
{
    [Fact]
    public async Task GivenEmptyDeskStatuses_GetAll_TReturnEmptyListOk()
    {
        var mock = new Mock<IDeskStatusService>();
        mock.Setup(service => service.FindAllAsync())
            .ReturnsAsync([]);
        
        var actual = await DeskStatusActions.GetAll(mock.Object);
        
        Assert.IsAssignableFrom<Ok<List<DeskStatusData>>>(actual);
    }
}