using backend.Models.DTO;

namespace backend.Services.Reservation;

public interface IReservationService
{
    Task<ReservationData> CreateAsync(ReservationCreateView createData, CancellationToken cancellationToken = default);

    Task CancelAsync(long reservationId, CancellationToken cancellationToken = default);
}