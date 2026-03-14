using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OtoTamir.CORE.Entities;
using OtoTamir.CORE.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace OtoTamir.DAL.Context
{
    public class DataContext : IdentityDbContext<Mechanic>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DataContext(DbContextOptions<DataContext> options, IHttpContextAccessor httpContextAccessor = null) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
    //    protected override void OnModelCreating(ModelBuilder modelBuilder)
    //    {
    //        base.OnModelCreating(modelBuilder);

    //        modelBuilder.Entity<Mechanic>()
    //      .HasOne(m => m.Treasury)
    //      .WithOne(t => t.Mechanic)
    //      .HasForeignKey<Mechanic>(m => m.TreasuryId);



    //        modelBuilder.Entity<Client>()
    //.HasIndex(c => new { c.MechanicId, c.PhoneNumber })
    //.IsUnique();

    //    }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.Entity<Mechanic>()
                .HasOne(m => m.Treasury)
                .WithOne(t => t.Mechanic)
                .HasForeignKey<Mechanic>(m => m.TreasuryId);

            builder.Entity<Client>()
                .HasIndex(c => new { c.MechanicId, c.PhoneNumber })
                .IsUnique();

            var currentUserId = _httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

           

            if (!string.IsNullOrEmpty(currentUserId))
            {
                
                builder.Entity<Client>().HasQueryFilter(x => !x.IsDeleted && x.MechanicId == currentUserId);

                builder.Entity<ServiceRecord>().HasQueryFilter(x => !x.IsDeleted);

                
                builder.Entity<Vehicle>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<Mechanic>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<TreasuryTransaction>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<Bank>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<BankCard>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<RepairComment>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<SparePart>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<Treasury>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<TransactionCategory>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<Symptom>().HasQueryFilter(x => !x.IsDeleted);
                builder.Entity<PosTerminal>().HasQueryFilter(x => !x.IsDeleted);

               
            }
            else
            {
               
                //builder.Entity<Client>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<ServiceRecord>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<Vehicle>().HasQueryFilter(x => !x.IsDeleted);

                //builder.Entity<Mechanic>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<TreasuryTransaction>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<Bank>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<BankCard>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<RepairComment>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<SparePart>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<Treasury>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<TransactionCategory>().HasQueryFilter(x => !x.IsDeleted);
                //builder.Entity<Symptom>().HasQueryFilter(x => !x.IsDeleted);
            }
        }

        public DbSet<Mechanic> Mechanics { get; set; }       
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ServiceRecord> ServiceRecords { get; set; }
        public DbSet<Symptom> Symptoms { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<PurchaseDetail> PurchaseDetails { get; set; }
        public DbSet<RepairComment> RepairComments { get; set; }
        public DbSet<SparePart> SpareParts { get; set; }
        
        public DbSet<Bank> Banks { get; set; }
        public DbSet<BankCard> BankCards { get; set; }
        public DbSet<TransactionCategory> TransactionCategories  { get; set; }
        public DbSet<TreasuryTransaction> Transactions  { get; set; }
        public DbSet<Treasury> Treasuries { get; set; }
        public DbSet<PosTerminal> PosTerminals { get; set; }

        
        private void SetBaseEntityDates()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                
                if (entry.Entity is BaseEntity entity)
                {
                    switch (entry.State)
                    {
                        case EntityState.Modified:
                            entity.ModifiedDate = DateTime.Now;
                            break;
                        case EntityState.Added:
                            entity.CreatedDate = DateTime.Now;
                            entity.ModifiedDate = DateTime.Now;
                            break;
                    }
                }
            }
        }

       
      

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var entries = ChangeTracker.Entries<BaseEntity>();

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedDate = DateTime.Now;
                    entry.Entity.IsDeleted = false;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.ModifiedDate = DateTime.Now;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    entry.Entity.ModifiedDate = DateTime.Now;
                }
            }
            var mechanicEntries = ChangeTracker.Entries<Mechanic>();
            foreach (var entry in mechanicEntries)
            {
                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }



}

