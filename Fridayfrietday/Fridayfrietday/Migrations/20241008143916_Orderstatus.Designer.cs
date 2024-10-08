﻿// <auto-generated />
using System;
using Fridayfrietday;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Fridayfrietday.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20241008143916_Orderstatus")]
    partial class Orderstatus
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Fridayfrietday.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<string>("Picture")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Frieten",
                            Picture = "Frietenmandje.png"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Snacks",
                            Picture = "snackslogo.png"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Dranken",
                            Picture = "DrinksLogo.png"
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "customer1@example.com"
                        },
                        new
                        {
                            Id = 2,
                            Email = "customer2@example.com"
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("OrderStatus")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PickupNumber")
                        .HasColumnType("int");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CustomerId = 1,
                            OrderDate = new DateTime(2024, 10, 8, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9597),
                            OrderStatus = "Opgehaald",
                            PickupNumber = 1,
                            TotalPrice = 11.0
                        },
                        new
                        {
                            Id = 2,
                            CustomerId = 2,
                            OrderDate = new DateTime(2024, 10, 8, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9606),
                            OrderStatus = "Opgehaald",
                            PickupNumber = 2,
                            TotalPrice = 3.5
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.OrderDetail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderId");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderDetails");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderId = 1,
                            ProductId = 1,
                            Quantity = 2
                        },
                        new
                        {
                            Id = 2,
                            OrderId = 1,
                            ProductId = 7,
                            Quantity = 1
                        },
                        new
                        {
                            Id = 3,
                            OrderId = 2,
                            ProductId = 2,
                            Quantity = 1
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.OrderDetailSauce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("OrderDetailId")
                        .HasColumnType("int");

                    b.Property<int>("SauceId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrderDetailId");

                    b.HasIndex("SauceId");

                    b.ToTable("OrderDetailSauces");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            OrderDetailId = 1,
                            SauceId = 1
                        },
                        new
                        {
                            Id = 2,
                            OrderDetailId = 1,
                            SauceId = 2
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("AllowsSauces")
                        .HasColumnType("bit");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<string>("ImageLink")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AllowsSauces = true,
                            CategoryId = 1,
                            ImageLink = "Frietenmandje.png",
                            Name = "Friet Groot",
                            Price = 3.5
                        },
                        new
                        {
                            Id = 2,
                            AllowsSauces = true,
                            CategoryId = 1,
                            ImageLink = "Frietenmandje.png",
                            Name = "Friet Medium",
                            Price = 3.0
                        },
                        new
                        {
                            Id = 3,
                            AllowsSauces = true,
                            CategoryId = 1,
                            ImageLink = "Frietenmandje.png",
                            Name = "Friet Klein",
                            Price = 2.5
                        },
                        new
                        {
                            Id = 4,
                            AllowsSauces = true,
                            CategoryId = 2,
                            ImageLink = "Bitterballen.png",
                            Name = "Bitterballen",
                            Price = 4.0
                        },
                        new
                        {
                            Id = 5,
                            AllowsSauces = true,
                            CategoryId = 2,
                            ImageLink = "frikandelspeciaal.png",
                            Name = "Frikandel Speciaal",
                            Price = 2.5
                        },
                        new
                        {
                            Id = 6,
                            AllowsSauces = true,
                            CategoryId = 2,
                            ImageLink = "frikandelXXL.png",
                            Name = "Frikandel XXL",
                            Price = 5.0
                        },
                        new
                        {
                            Id = 7,
                            AllowsSauces = false,
                            CategoryId = 3,
                            ImageLink = "Cola.png",
                            Name = "Cola",
                            Price = 3.0
                        },
                        new
                        {
                            Id = 8,
                            AllowsSauces = false,
                            CategoryId = 3,
                            ImageLink = "fanta.png",
                            Name = "Fanta",
                            Price = 3.0
                        },
                        new
                        {
                            Id = 9,
                            AllowsSauces = false,
                            CategoryId = 3,
                            ImageLink = "ColaLight.png",
                            Name = "Cola Light",
                            Price = 3.0
                        },
                        new
                        {
                            Id = 10,
                            AllowsSauces = false,
                            CategoryId = 3,
                            ImageLink = "ColaZero.png",
                            Name = "Cola Zero",
                            Price = 3.0
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<decimal>("Stars")
                        .HasPrecision(2, 1)
                        .HasColumnType("decimal(2,1)");

                    b.HasKey("Id");

                    b.ToTable("Reviews");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2024, 9, 28, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9902),
                            Description = "Heerlijke frietjes!",
                            Name = "John Doe",
                            Stars = 4.5m
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2024, 10, 3, 16, 39, 14, 906, DateTimeKind.Local).AddTicks(9914),
                            Description = "Snacks waren goed, maar had liever meer saus.",
                            Name = "Jane Smith",
                            Stars = 3.5m
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.Sauce", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Sauces");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Mayonaise",
                            Price = 0.5
                        },
                        new
                        {
                            Id = 2,
                            Name = "Ketchup",
                            Price = 0.5
                        },
                        new
                        {
                            Id = 3,
                            Name = "Curry",
                            Price = 0.5
                        });
                });

            modelBuilder.Entity("Fridayfrietday.Models.Order", b =>
                {
                    b.HasOne("Fridayfrietday.Models.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("Fridayfrietday.Models.OrderDetail", b =>
                {
                    b.HasOne("Fridayfrietday.Models.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fridayfrietday.Models.Product", "Product")
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Fridayfrietday.Models.OrderDetailSauce", b =>
                {
                    b.HasOne("Fridayfrietday.Models.OrderDetail", "OrderDetail")
                        .WithMany("SelectedSauces")
                        .HasForeignKey("OrderDetailId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Fridayfrietday.Models.Sauce", "Sauce")
                        .WithMany()
                        .HasForeignKey("SauceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderDetail");

                    b.Navigation("Sauce");
                });

            modelBuilder.Entity("Fridayfrietday.Models.Product", b =>
                {
                    b.HasOne("Fridayfrietday.Models.Category", "Category")
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Fridayfrietday.Models.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Fridayfrietday.Models.Order", b =>
                {
                    b.Navigation("OrderDetails");
                });

            modelBuilder.Entity("Fridayfrietday.Models.OrderDetail", b =>
                {
                    b.Navigation("SelectedSauces");
                });
#pragma warning restore 612, 618
        }
    }
}
