using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ScooterRent.WebApi.Database
{
    public class Context : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Scooter> Scooters { get; set; }
        public DbSet<Rental> Rentals { get; set; }
        public DbSet<Defect> Defects { get; set; }
        
        public Context(DbContextOptions<Context> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
               .HasMany(g => g.Rentals) 
               .WithOne(g => g.Client) 
               .HasForeignKey(s => s.ClientId);

            modelBuilder.Entity<Scooter>()
               .HasMany(g => g.Rentals)
               .WithOne(g => g.Scooter)
               .HasForeignKey(s => s.ScooterId);

            modelBuilder.Entity<Scooter>()
               .HasOne(g => g.Defect)
               .WithOne(g => g.Scooter)
               .HasForeignKey<Scooter>(g => g.DefectId);

            modelBuilder.Entity<Client>()
               .HasOne(g => g.Scooter)
               .WithOne(g => g.Client)
               .HasForeignKey<Client>(g => g.ScooterId);
        }
    }
}
