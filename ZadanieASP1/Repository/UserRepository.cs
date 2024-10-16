using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ZadanieASP1.Data;
using ZadanieASP1.Models;

namespace ZadanieASP1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly TravelAgencyContext _context;

        public UserRepository(TravelAgencyContext context)
        {
            _context = context;
        }

        // Metoda do pobierania wszystkich użytkowników
        public async Task<IEnumerable<IdentityUser>> GetAllAsync()
        {
            // Pobieranie użytkowników z tabeli AspNetUsers
            return await _context.Users.ToListAsync();
        }

        // Metoda do pobierania użytkownika po identyfikatorze
        public async Task<IdentityUser?> GetByIdAsync(string userId)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Id == userId);

            return user;
        }

        // Metoda dodawania użytkownika (możliwe, że będziesz chciał użyć UserManager do tego celu)
        public async Task InsertAsync(IdentityUser user)
        {
            await _context.Users.AddAsync(user);
        }

        // Metoda aktualizacji danych użytkownika
        public async Task UpdateAsync(IdentityUser user)
        {
            _context.Users.Update(user);
        }

        // Metoda usuwania użytkownika
        public async Task DeleteAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        // Zapisanie zmian do bazy
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
