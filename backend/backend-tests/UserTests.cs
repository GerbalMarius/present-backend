using backend.Models.DTO;
using backend.RouteActions;
using backend.Services.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace backend_tests;

public class UserTests : IDisposable 
{
    private readonly Mock<IUserService> _userService = new();


    [Fact]
    public async Task UserActionsGetCurrentUserAsync_ReturnsOkUser()
    {
        var user = new UserData(5, "mariukas.ambrazevicius@gmail.com", "Marius", "Ambrazevicius");
        _userService.Setup(s => s.GetCurrentUserAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(user);
        
        
        IResult result = await UserActions.GetCurrentUserAsync(_userService.Object);
        _userService.Verify(s => s.GetCurrentUserAsync(It.IsAny<CancellationToken>()), Times.Once);
        
        var ok = Assert.IsType<Ok<UserData>>(result);
        
        Assert.True(
            ok.Value == user && ok.StatusCode == StatusCodes.Status200OK
            );
        
    }

    [Fact]
    public async Task UserActionsGetReservationDataByUserAsync_ReturnsOkList()
    {
        const long userId = 1;
        
        DateTime now = new DateTime(2003, 9, 5);

        List<ReservationData> reservations =
        [
            new(1, userId, 2, now, now.AddDays(5)),
            new(2, userId, 3, now, now.AddDays(10))
        ];
        
        _userService.Setup(s => s.GetReservationDataByUserAsync(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(reservations);
        
        IResult result = await UserActions.GetReservationDataByUserAsync(userId, _userService.Object);
        _userService.Verify(s => s.GetReservationDataByUserAsync(userId, It.IsAny<CancellationToken>()), Times.Once);
        
        var ok = Assert.IsType<Ok<List<ReservationData>>>(result);
        
        Assert.NotNull(ok.Value);
        Assert.True(ok.Value.SequenceEqual(reservations));
        
        Assert.Equal(StatusCodes.Status200OK, ok.StatusCode);
        
        
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _userService.VerifyNoOtherCalls();
        _userService.Reset();
    }
}