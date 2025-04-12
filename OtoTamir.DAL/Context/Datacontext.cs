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
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=DESKTOP-OM2N2UM; Database=OtoTamirDB; Integrated Security=true; TrustServerCertificate=True;");
        //}
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

         
            


        }
        public DbSet<Mechanic> Mechanics { get; set; }       
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<Symptom> Symptom { get; set; }
        public DbSet<Client> Clients { get; set; }
        public override int SaveChanges()
        {
           
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                {
                    
                    entry.Property("ModifiedDate").CurrentValue = DateTime.Now;
                }
            }

            return base.SaveChanges();
        }



    }
}
