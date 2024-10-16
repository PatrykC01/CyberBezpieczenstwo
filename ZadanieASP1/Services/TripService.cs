using ZadanieASP1.Models;
using ZadanieASP1.Repository;

namespace ZadanieASP1.Services
{
    public class TripService : ITripService
    {
        private readonly ITripRepository _tripRepo;
        public TripService(ITripRepository tripRepo) 
        {
            _tripRepo= tripRepo;
        }

        public Task<IEnumerable<Trip>> GetAllAsync() => _tripRepo.GetAllAsync();
        public Task<Trip?> GetByIdAsync(int TripID) => _tripRepo.GetByIdAsync(TripID);
        public Task InsertAsync(Trip trip) => _tripRepo.InsertAsync(trip);
        public Task UpdateAsync(Trip trip) => _tripRepo.UpdateAsync(trip);
        public Task DeleteAsync(int tripId) => _tripRepo.DeleteAsync(tripId);
        public Task SaveAsync() => _tripRepo.SaveAsync();
        public async Task<IEnumerable<Trip>> GetTripByNameAsync(string searchString)
        {
            var allTrips = await GetAllAsync();
            searchString = searchString.ToLower();
            return allTrips.Where(t => t.Title.ToLower().Contains(searchString)
                                       || t.Description.ToLower().Contains(searchString)
                                       || t.Price.ToString().ToLower().Contains(searchString)
                                       || t.TripDate.ToString().ToLower().Contains(searchString)
                                       || t.Duration.ToLower().Contains(searchString));
        }


        public async Task<PaginatedList<Trip>> GetTripsPerPageAsync(int page, int tripPerPage, string filter = "")
        {
            var allTrips = await _tripRepo.GetAllAsync();
            
            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                allTrips = allTrips.Where(t => t.Title.ToLower().Contains(filter)
                                               || t.Description.ToLower().Contains(filter)
                                               || t.Price.ToString().Contains(filter)
                                               || t.TripDate.ToString().Contains(filter)
                                               || t.Duration.ToLower().Contains(filter));
            }

            var count = allTrips.Count();

            var items = allTrips.Skip((page - 1) * tripPerPage)
                                 .Take(tripPerPage)
                                 .ToList();

            return new PaginatedList<Trip>(items, count, page, tripPerPage);
        }








    }
}
