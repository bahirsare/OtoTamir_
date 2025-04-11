using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtoTamir.DAL.Context
{
    public class DataContext : IdentityDbContext<Mechanic>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OM2N2UM; Database=OtoTamirDB; Integrated Security=true; TrustServerCertificate=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<ClientMechanic>()
                .HasKey(cm => new { cm.ClientId, cm.MechanicId });

            modelBuilder.Entity<ClientMechanic>()
                .HasOne(cm => cm.Client)
                .WithMany(c => c.ClientMechanics)
                .HasForeignKey(cm => cm.ClientId);

            modelBuilder.Entity<ClientMechanic>()
                .HasOne(cm => cm.Mechanic)
                .WithMany(m => m.ClientMechanics)
                .HasForeignKey(cm => cm.MechanicId);

            
            modelBuilder.Entity<Vehicle>()
                .HasOne(v => v.Client)
                .WithMany(c => c.Vehicles)
                .HasForeignKey(v => v.ClientId);

            
            modelBuilder.Entity<ServiceRecord>()
                .HasOne(sr => sr.Vehicle)
                .WithMany(v => v.ServiceRecords)
                .HasForeignKey(sr => sr.VehicleId);

            modelBuilder.Entity<Symptom>()
                .HasOne(s => s.Vehicle)
                .WithMany(v => v.Symptoms)
                .HasForeignKey(s => s.VehicleId);
        }

        public DbSet<Mechanic> Mechanics { get; set; }
       
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<Symptom> Symptom { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
