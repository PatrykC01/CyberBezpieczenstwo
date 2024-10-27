using ZadanieASP1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ZadanieASP1.Data
{
    public class TravelAgencyContext : IdentityDbContext<ApplicationUser> // Zmiana z IdentityUser na ApplicationUser
    {
        public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
        {
            DbInitializer.Initialize(this);
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<PasswordHistory> PasswordHistories { get; set; } // Dodaj DbSet dla PasswordHistory


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {   
            modelBuilder.Entity<PasswordHistory>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.HashedPassword).IsRequired();
                entity.Property(e => e.DateChanged).IsRequired();
                entity.HasOne<ApplicationUser>()
                      .WithMany(u => u.PasswordHistories)
                      .HasForeignKey(e => e.UserId); // Dodaj nowe pole
            });
            modelBuilder.Entity<Trip>().ToTable("Trip");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Customer>().ToTable("Customer");

            base.OnModelCreating(modelBuilder);
        }


    }
}
