using BookingSystemApi.Models;
using Microsoft.EntityFrameworkCore;
namespace BookingSystemApi.Data
{
    public class appDbContext : DbContext
    {
        public DbSet<Booking> Bookings { get; set; }

        public appDbContext(DbContextOptions<appDbContext> options)
            : base(options) { }
    }
}
