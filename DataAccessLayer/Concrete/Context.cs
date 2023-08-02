using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Concrete
{
    public class Context : DbContext
    {
        public DbSet<Demand> Demands { get; set; }
        public DbSet<Firm> Firms { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<PackageStatus> PackageStatuses { get; set; }
        public DbSet<Store> Stores { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<DemandStatus> DemandStatuses { get; set; }
        public DbSet<DemandTracking> DemandTrackings { get; set; }
        public DbSet<PackageTracking> PackageTrackings { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<User>()
                .HasRequired(u => u.Firm)
                .WithMany(f => f.Users)
                .HasForeignKey(u => u.FirmId)
                .WillCascadeOnDelete(false);
            
            base.OnModelCreating(modelBuilder);
        }
    }
    
}
