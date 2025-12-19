using backend.Models.DTO;
using backend.RouteActions;
using backend.Services.Desk;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace backend_tests;


public class DeskTests : IDisposable
{
    private readonly Mock<IDeskService> _deskService = new();

    [Fact]
    public async Task DeskActionsGetAllAsync_WithEmptyList_ReturnsOkList()
    {
        _deskService.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                       .ReturnsAsync(new List<DeskData>());
        
        IResult result = await DeskActions.GetAllAsync(_deskService.Object);
       _deskService.Verify(
           s => s.GetAllAsync(It.IsAny<CancellationToken>()), 
           Times.Once
           );
       
       var okResult = Assert.IsType<Ok<List<DeskData>>>(result);
       
       Assert.NotNull(okResult.Value);
       
       Assert.Empty(okResult.Value);
       
       Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
       
    }

    [Fact]
    public async Task DeskActionsGetByIdAsync_WhenFound_ReturnsOkDesk()
    {
        DeskData deskData = new(1, true, false, null, null);
        
        _deskService.Setup(
                s => s.GetByIdAsync(1, It.IsAny<CancellationToken>())
                ).ReturnsAsync(deskData);
        
        IResult result = await DeskActions.GetByIdAsync(1, _deskService.Object);
        _deskService.Verify(
            s => s.GetByIdAsync(1, It.IsAny<CancellationToken>()), 
            Times.Once
            );
        
        var okResult = Assert.IsType<Ok<DeskData>>(result);
        
        Assert.NotNull(okResult.Value);
        Assert.Equal(deskData, okResult.Value);
        Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
    }

    [Fact]
    public async Task DeskActionsGetByIdAsync_WhenNotFound_ReturnsNotFound()
    {
        _deskService.Setup(
                s => s.GetByIdAsync(-1, It.IsAny<CancellationToken>())
                ).ReturnsAsync(DeskData.Empty);
        
        IResult result = await DeskActions.GetByIdAsync(-1, _deskService.Object);
        _deskService.Verify(
            s => s.GetByIdAsync(-1, It.IsAny<CancellationToken>()), 
            Times.Once
            );

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