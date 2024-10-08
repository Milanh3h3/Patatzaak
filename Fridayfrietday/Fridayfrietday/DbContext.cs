using Fridayfrietday.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Fridayfrietday
{
    public class DBContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
        public DbSet<OrderDetailSauce> OrderDetailSauces { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Sauce> Sauces { get; set; }
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

            modelBuilder.Entity<Review>()
                .Property(r => r.Stars)
                .HasPrecision(2, 1);

            base.OnModelCreating(modelBuilder);

            // Category seeding
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Frieten"},
                new Category { Id = 2, Name = "Snacks"},
                new Category { Id = 3, Name = "Dranken"}
            );

            // Product seeding
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Friet Groot", CategoryId = 1, Price = 3.5, AllowsSauces = true, ImageLink = "Frietenmandje.png" },
                new Product { Id = 2, Name = "Friet Medium", CategoryId = 1, Price = 3.0, AllowsSauces = true, ImageLink = "Frietenmandje.png" },
                new Product { Id = 3, Name = "Friet Klein", CategoryId = 1, Price = 2.5, AllowsSauces = true, ImageLink = "Frietenmandje.png" },
                new Product { Id = 4, Name = "Bitterballen", CategoryId = 2, Price = 4.0, AllowsSauces = true, ImageLink = "Bitterballen.png" },
                new Product { Id = 5, Name = "Frikandel Speciaal", CategoryId = 2, Price = 2.5, AllowsSauces = true, ImageLink = "frikandelspeciaal.png" },
                new Product { Id = 6, Name = "Frikandel XXL", CategoryId = 2, Price = 5.0, AllowsSauces = true, ImageLink = "frikandelXXL.png" },
                new Product { Id = 7, Name = "Cola", CategoryId = 3, Price = 3.0, AllowsSauces = false, ImageLink = "Cola.png" },
                new Product { Id = 8, Name = "Fanta", CategoryId = 3, Price = 3.0, AllowsSauces = false, ImageLink = "fanta.png" },
                new Product { Id = 9, Name = "Cola Light", CategoryId = 3, Price = 3.0, AllowsSauces = false, ImageLink = "ColaLight.png" },
                new Product { Id = 10, Name = "Cola Zero", CategoryId = 3, Price = 3.0, AllowsSauces = false, ImageLink = "ColaZero.png" }
            );

            // Customer seeding
            modelBuilder.Entity<Customer>().HasData(
                new Customer { Id = 1, Email = "customer1@example.com" },
                new Customer { Id = 2, Email = "customer2@example.com" }
            );

            // Order seeding
            modelBuilder.Entity<Order>().HasData(
                new Order { Id = 1, TotalPrice = 11.0, CustomerId = 1, OrderDate = DateTime.Now, PickupNumber = 1, OrderStatus = "Opgehaald" },
                new Order { Id = 2, TotalPrice = 3.5, CustomerId = 2, OrderDate = DateTime.Now, PickupNumber = 2, OrderStatus = "Opgehaald" }
            );

            // OrderDetail seeding
            modelBuilder.Entity<OrderDetail>().HasData(
                new OrderDetail { Id = 1, OrderId = 1, ProductId = 1, Quantity = 2 },
                new OrderDetail { Id = 2, OrderId = 1, ProductId = 7, Quantity = 1 },
                new OrderDetail { Id = 3, OrderId = 2, ProductId = 2, Quantity = 1 }
            );

            // Sauce seeding
            modelBuilder.Entity<Sauce>().HasData(
                new Sauce { Id = 1, Name = "Mayonaise", Price = 0.5 },
                new Sauce { Id = 2, Name = "Ketchup", Price = 0.5 },
                new Sauce { Id = 3, Name = "Curry", Price = 0.5 }
            );

            // OrderDetailSauce seeding
            modelBuilder.Entity<OrderDetailSauce>().HasData(
                new OrderDetailSauce { Id = 1, OrderDetailId = 1, SauceId = 1 },
                new OrderDetailSauce { Id = 2, OrderDetailId = 1, SauceId = 2 }
            );

            // Review seeding
            modelBuilder.Entity<Review>().HasData(
                new Review { Id = 1, Name = "John Doe", Description = "Heerlijke frietjes!", Stars = 4.5m, Date = DateTime.Now.AddDays(-10) },
                new Review { Id = 2, Name = "Jane Smith", Description = "Snacks waren goed, maar had liever meer saus.", Stars = 3.5m, Date = DateTime.Now.AddDays(-5) }
            );
        }
    }
}
