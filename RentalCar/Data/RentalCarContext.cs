using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RentalCar.Models;

namespace RentalCar.Data
{
    public class RentalCarContext:DbContext
    {
        public RentalCarContext(DbContextOptions<RentalCarContext> options) : base(options) { }

        public DbSet<AutoModel> Auto { get; set; }
        public DbSet<DriverModel> Drivers { get; set; }
        public DbSet<OrderModel> Orders { get; set; }
        public DbSet<PictureModel> Pictures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AutoModel>().ToTable("Auto");
            modelBuilder.Entity<DriverModel>().ToTable("Drivers");
            modelBuilder.Entity<OrderModel>().ToTable("Orders");
            modelBuilder.Entity<PictureModel>().ToTable("Pictures");
        }
    }
}
