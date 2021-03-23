using IsparkDoluluk.DataAccess.Concrete.Mapping;
using IsparkDoluluk.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace IsparkDoluluk.DataAccess.Concrete.Context
{
    public class IsparkDolulukDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql("server=localhost;port=3306;database=isparkdoluluk;user=root;password=12345");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ParkPlaceMap());
            modelBuilder.ApplyConfiguration(new LiveCapacityMap());
            modelBuilder.ApplyConfiguration(new AppUserMap());
            modelBuilder.ApplyConfiguration(new AppRoleMap());
            modelBuilder.ApplyConfiguration(new AppUserRoleMap());
        }

        public DbSet<ParkPlace> ParkPlaces { get; set; }
        public DbSet<LiveCapacity> LiveCapacity { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<AppRole> AppRoles { get; set; }
        public DbSet<AppUserRole> AppUserRoles { get; set; }
    }
}
