using Microsoft.AspNetCore.Identity;
using ZadanieASP1.Models;

namespace ZadanieASP1.Repository
{
    public interface IUserRepository
    {
        
        Task<IEnumerable<ApplicationUser>> GetAllAsync();
       
        Task<ApplicationUser?> GetByIdAsync(string userId);
        
        Task InsertAsync(ApplicationUser identityUser);
        
        Task UpdateAsync(ApplicationUser identityUser);
      
        Task DeleteAsync(string userId);
        
        Task SaveAsync();


    }
}
