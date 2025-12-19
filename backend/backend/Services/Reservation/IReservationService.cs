using backend.Models.DTO;

namespace backend.Services.Reservation;

public interface IReservationService
{
    Task<ReservationData> CreateAsync(ReservationCreateView createData, CancellationToken cancellationToken = default);

    Task<ReservationData> CancelForADayAsync(long reservationId, CancellationToken cancellationToken = default);

    Task<ReservationData> CancelAsync(long reservationId, CancellationToken cancellationToken = default);
}