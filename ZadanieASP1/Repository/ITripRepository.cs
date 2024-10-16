using ZadanieASP1.Models;

namespace ZadanieASP1.Repository
{
    public interface ITripRepository
    {

        Task<IEnumerable<Trip>> GetAllAsync();

        Task<Trip?> GetByIdAsync(int TripID);

        Task InsertAsync(Trip trip);

        Task UpdateAsync(Trip trip);

        Task DeleteAsync(int tripId);

        Task SaveAsync();
    }
}
