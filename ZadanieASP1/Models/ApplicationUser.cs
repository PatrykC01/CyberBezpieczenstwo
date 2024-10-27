using Microsoft.AspNetCore.Identity;

namespace ZadanieASP1.Models
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime? PasswordLastChangedDate { get; set; }
        public DateTime? PasswordExpiryDate { get; set; }
        public bool DisablePasswordRestrictions { get; set; }
        public virtual ICollection<PasswordHistory> PasswordHistories { get; set; } = new List<PasswordHistory>();
    }

    public class PasswordHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string HashedPassword { get; set; }
        public DateTime DateChanged { get; set; }
    }
}
