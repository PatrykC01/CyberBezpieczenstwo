using ZadanieASP1.Models;

namespace ZadanieASP1.Repository
{
    public interface IReservationRepository
    {

        Task<IEnumerable<Reservation>> GetAllAsync();

        Task<Reservation?> GetByIdAsync(int ReservationID);

        Task InsertAsync(Reservation reservation);

        Task UpdateAsync(Reservation reservation);

        Task DeleteAsync(int reservationId);

        Task SaveAsync();
    }
}
