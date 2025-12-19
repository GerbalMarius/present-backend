using backend.Models.DTO;
using backend.RouteActions;
using backend.Services.Desk;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace backend_tests;

// In Xunit setups are constructors, teardown is Dispose()
public class DeskTests : IDisposable
{
    private readonly Mock<IDeskService> _deskService = new();

    [Fact]
    public async Task DeskActionsGetAllAsync_WithEmptyList_ReturnsOkList()
    {
        _deskService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new List<DeskData>());
        
        IResult result = await DeskActions.GetAllAsync(_deskService.Object);
       _deskService.Verify(s => s.GetAllAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        Assert.True(
            result is Ok<List<DeskData>> {Value.Count: 0, StatusCode: StatusCodes.Status200OK }
            );
       
    }

    [Fact]
    public async Task DeskActionsGetByIdAsync_WhenFound_ReturnsOkDesk()
    {
        DeskData deskData = new(1, true, false, null, null);
        
        _deskService.Setup(s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(deskData);
        
        IResult result = await DeskActions.GetByIdAsync(1, _deskService.Object);
        _deskService.Verify(
            s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()), 
            Times.Once);
        
        
        Assert.True(
            result is Ok<DeskData> deskResult && deskResult.Value == deskData
            && deskResult.StatusCode == StatusCodes.Status200OK
            );
    }

    [Fact]
    public async Task DeskActionsGetByIdAsync_WhenNotFound_ReturnsNotFound()
    {
        _deskService.Setup(s => s.GetByIdAsync(-1, It.IsAny<CancellationToken>()))
            .ReturnsAsync(DeskData.Empty);
        
        IResult result = await DeskActions.GetByIdAsync(-1, _deskService.Object);
        _deskService.Verify(s => s.GetByIdAsync(-1, It.IsAny<CancellationToken>()), Times.Once);

        var notFound = Assert.IsType<NotFound<Dictionary<string, object?>>>(result);    

        Assert.NotNull(notFound.Value);
        
        Assert.Equal("Desk not found", notFound.Value["message"]);
        
        Assert.Equal(StatusCodes.Status404NotFound, notFound.StatusCode);
        
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _deskService.VerifyNoOtherCalls();
        _deskService.Reset();
    }
}