using Fridayfrietday.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Fridayfrietday
{
    public class DBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sauce> Sauces { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailSauce> OrderDetailSauces { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=FrituurDb; Integrated Security=SSPI; TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderDetailSauce>()
                .HasOne(ods => ods.OrderDetail)
                .WithMany(od => od.SelectedSauces)
                .HasForeignKey(ods => ods.OrderDetailId);

            modelBuilder.Entity<OrderDetailSauce>()
                .HasOne(ods => ods.Sauce)
                .WithMany()
                .HasForeignKey(ods => ods.SauceId);

            base.OnModelCreating(modelBuilder);
        }
        public DbSet<Fridayfrietday.Models.Review> Review { get; set; } = default!;
    }
}
