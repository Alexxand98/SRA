using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Models.Entity;

namespace SRA.ApiRest.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<FranjaHoraria> FranjasHorarias { get; set; }
        public DbSet<DiaNoLectivo> DiasNoLectivos { get; set; }
    }
}