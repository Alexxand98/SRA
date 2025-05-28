using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Models.Entity;

namespace SRA.ApiRest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Reserva> Reservas => Set<Reserva>();
    }
}