using backend_tests.Helpers;
using backend.Models.DTO;
using backend.RouteActions;
using backend.Services.Reservation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;

namespace backend_tests;

public class ReservationTests : IDisposable
{
    private readonly Mock<IReservationService> _reservationService = new();

    [Fact]
    public async Task ReservationActionsCreateReservation_WithValidData_ReturnsCreated()
    {
        var now = DateTime.Now;

        var viewData = new ReservationCreateView(
            1,
            2,
            now,
            now.AddDays(5)
        );

        var created = new ReservationData(
            1,
            viewData.UserId!.Value,
            viewData.DeskId!.Value,
            viewData.ReservedFrom!.Value,
            viewData.ReservedTo!.Value
        );

        _reservationService.Setup(s => s.CreateAsync(viewData, It.IsAny<CancellationToken>())
        ).ReturnsAsync(created);


        var result = await ReservationActions.CreateAsync(viewData, _reservationService.Object);

        _reservationService.Verify(
            s => s.CreateAsync(viewData, It.IsAny<CancellationToken>()),
            Times.Once
        );

        var validationErrors = await Http.Validate(result);

        Assert.Empty(validationErrors);

        var createdAt = Assert.IsType<CreatedAtRoute<ReservationData>>(result);
        Assert.NotNull(createdAt.Value);
        Assert.Equal(viewData.UserId, createdAt.Value.UserId);
        Assert.Equal(viewData.DeskId, createdAt.Value.DeskId);

        Assert.Equal(StatusCodes.Status201Created, createdAt.StatusCode);
    }

    [Fact]
    public async Task ReservationActionsCreateReservation_WithInvalidData_ReturnsUnprocessableEntity()
    {
        var now = DateTime.Now;

        var invalid = new ReservationCreateView(
            null,
            0,
            now.AddDays(-1),
            now.AddDays(-5)
        );

        var body = await Http.Validate(invalid);
        Assert.NotNull(body);

        Assert.Contains("errors", body);

        var validationErrors = Assert.IsType<Dictionary<string, string>>(body["errors"]);

        Assert.Equal(4, validationErrors.Count);
        Assert.Equal("There were validation errors", body["message"]);

        Assert.Equal(StatusCodes.Status422UnprocessableEntity, body["status"]);
    }


    [Fact]
    public async Task ReservationActionsCancelReservation_WhenFound_ReturnsNoContent()
    {
        var now = DateTime.Now;
        const long reservationId = 5;

        var reservationData = new ReservationData(
            reservationId,
            1,
            2,
            now,
            now.AddDays(5)
        );

        _reservationService
            .Setup(s => s.CancelAsync(reservationId, It.IsAny<CancellationToken>())
            ).ReturnsAsync(reservationData);

        var result = await ReservationActions.CancelAsync(reservationId, _reservationService.Object);

        _reservationService.Verify(
            s => s.CancelAsync(reservationId, It.IsAny<CancellationToken>()),
            Times.Once
        );

        var noContent = Assert.IsType<NoContent>(result);
        Assert.Equal(StatusCodes.Status204NoContent, noContent.StatusCode);
    }

    [Fact]
    public async Task ReservationActionsCancelReservation_WhenNotFound_ReturnsNotFound()
    {
        const long reservationId = 5;

        _reservationService
            .Setup(s => s.CancelAsync(reservationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ReservationData.Empty);

        var result = await ReservationActions.CancelAsync(reservationId, _reservationService.Object);
        _reservationService.Verify(s => s.CancelAsync(reservationId, It.IsAny<CancellationToken>()), Times.Once);

        var notFound = Assert.IsType<NotFound<Dictionary<string, object?>>>(result);
        Assert.NotNull(notFound.Value);
        Assert.Equal("Reservation not found", notFound.Value["message"]);
        Assert.Equal(StatusCodes.Status404NotFound, notFound.StatusCode);
    }


    [Fact]
    public async Task ReservationActionsCancelForADay_WhenFound_ReturnsNoContent()
    {
        var now = DateTime.Now;

        const long reservationId = 7;

        var returned = new ReservationData(
            reservationId,
            1,
            2,
            now.AddDays(1),
            now.AddDays(5)
        );

        _reservationService
            .Setup(s => s.CancelForADayAsync(reservationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(returned);

        var result = await ReservationActions.CancelForADayAsync(reservationId, _reservationService.Object);
        _reservationService.Verify(
            s => s.CancelForADayAsync(reservationId, It.IsAny<CancellationToken>()),
            Times.Once
        );

        var noContent = Assert.IsType<NoContent>(result);

        Assert.Equal(StatusCodes.Status204NoContent, noContent.StatusCode);
    }

    [Fact]
    public async Task ReservationActionsCancelForADay_WhenNotFound_ReturnsNotFound()
    {
        const long reservationId = 7;

        _reservationService
            .Setup(s => s.CancelForADayAsync(reservationId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(ReservationData.Empty);

        var result = await ReservationActions.CancelForADayAsync(reservationId, _reservationService.Object);

        _reservationService.Verify(
            s => s.CancelForADayAsync(reservationId, It.IsAny<CancellationToken>()),
            Times.Once
        );

        var notFound = Assert.IsType<NotFound<Dictionary<string, object?>>>(result);
        Assert.NotNull(notFound.Value);
        Assert.Equal("Reservation not found", notFound.Value["message"]);
        Assert.Equal(StatusCodes.Status404NotFound, notFound.StatusCode);
    }
    
    public void Dispose()
    {
        GC.SuppressFinalize(this);
        _reservationService.VerifyNoOtherCalls();
        _reservationService.Reset();
    }
}