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
    public class DataContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=DESKTOP-OM2N2UM; Database=OtoTamirDB; User Id=sa; Pwd=1; TrustServerCertificate=True;");
        }

        public DbSet<Mechanic> Mechanics { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleFault> VehicleFaults { get; set; }
        public DbSet<VehicleModel> VehicleModels { get; set; }
        public DbSet<Client> Clients { get; set; }
    }
}
