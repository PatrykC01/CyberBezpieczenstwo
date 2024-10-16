using Microsoft.AspNetCore.Identity;
using ZadanieASP1.Models;

namespace ZadanieASP1.Repository
{
    public interface IUserRepository
    {
        
        Task<IEnumerable<IdentityUser>> GetAllAsync();
       
        Task<IdentityUser?> GetByIdAsync(string userId);
        
        Task InsertAsync(IdentityUser identityUser);
        
        Task UpdateAsync(IdentityUser identityUser);
      
        Task DeleteAsync(string userId);
        
        Task SaveAsync();
    }
}
