using ZadanieASP1.Models;
using Microsoft.EntityFrameworkCore;
using ZadanieASP1.Data;

namespace ZadanieASP1.Repository
{
    public class ReservationRepository : IReservationRepository
    {

        private readonly TravelAgencyContext _context;


        public ReservationRepository(TravelAgencyContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.Reservations.Include(e => e.Customer).Include(e => e.Trip).ToListAsync();
        }


        public async Task<Reservation?> GetByIdAsync(int ReservationID)
        {
            var reservation = await _context.Reservations
               .Include(e => e.Customer).Include(e => e.Trip)
               .FirstOrDefaultAsync(m => m.ReservationID == ReservationID);

            return reservation;
        }


        public async Task InsertAsync(Reservation reservation)
        {
            await _context.Reservations.AddAsync(reservation);
        }


        public async Task UpdateAsync(Reservation reservation)
        {
            _context.Reservations.Update(reservation);
        }


        public async Task DeleteAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
            }
        }


        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
