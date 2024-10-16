using ZadanieASP1.Models;

namespace ZadanieASP1.Services
{
    public interface ITripService
    {
        Task<IEnumerable<Trip>> GetAllAsync();

        Task<Trip?> GetByIdAsync(int TripID);

        Task InsertAsync(Trip trip);

        Task UpdateAsync(Trip trip);

        Task DeleteAsync(int tripId);

        Task SaveAsync();
        Task <IEnumerable<Trip>> GetTripByNameAsync(string name);
        Task<PaginatedList<Trip>> GetTripsPerPageAsync(int page, int tripPerPage, string filter = "");


    }
}
