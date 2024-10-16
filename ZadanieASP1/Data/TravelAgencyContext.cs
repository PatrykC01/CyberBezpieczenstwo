using ZadanieASP1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace ZadanieASP1.Data
{
    public class TravelAgencyContext : IdentityDbContext<IdentityUser>
    {
        public TravelAgencyContext(DbContextOptions<TravelAgencyContext> options) : base(options)
        {
            DbInitializer.Initialize(this);
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Trip>().ToTable("Trip");
            modelBuilder.Entity<Reservation>().ToTable("Reservation");
            modelBuilder.Entity<Customer>().ToTable("Customer");

            base.OnModelCreating(modelBuilder);
        }
    }
}
