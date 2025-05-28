using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Models.Entity;

namespace SRA.ApiRest.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Reserva> Reservas => Set<Reserva>();
    }
}