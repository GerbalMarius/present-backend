using backend.Models.DTO;

namespace backend.Services.Reservation;

public interface IReservationService
{
    Task<List<ReservationData>> GetAllByUserAsync(long userId, CancellationToken cancellationToken = default);
    
    Task<Models.Reservation> CreateAsync(ReservationData reservationData, CancellationToken cancellationToken = default);
}