using ZadanieASP1.Models;
using Microsoft.EntityFrameworkCore;
using ZadanieASP1.Data;

namespace ZadanieASP1.Repository
{
    public class TripRepository : ITripRepository
    {

        private readonly TravelAgencyContext _context;


        public TripRepository(TravelAgencyContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Trip>> GetAllAsync()
        {
            return await _context.Trips.Include(e => e.Reservations).ToListAsync();
        }


        public async Task<Trip?> GetByIdAsync(int TripID)
        {
            var trip = await _context.Trips
               .Include(e => e.Reservations)
               .FirstOrDefaultAsync(m => m.TripID == TripID);

            return trip;
        }


        public async Task InsertAsync(Trip trip)
        {
            await _context.Trips.AddAsync(trip);
        }


        public async Task UpdateAsync(Trip trip)
        {
            _context.Trips.Update(trip);
        }


        public async Task DeleteAsync(int tripId)
        {
            var trip = await _context.Trips.FindAsync(tripId);
            if (trip != null)
            {
                _context.Trips.Remove(trip);
            }
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
