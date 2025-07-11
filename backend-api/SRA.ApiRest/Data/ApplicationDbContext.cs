﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SRA.ApiRest.Models.Entity;

namespace SRA.ApiRest.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<Profesor> Profesores { get; set; }
        public DbSet<FranjaHoraria> FranjasHorarias { get; set; }
        public DbSet<DiaNoLectivo> DiasNoLectivos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Reserva>()
                .Property(r => r.Estado)
                .HasConversion<string>();
        }
    }
}
