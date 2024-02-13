﻿// <auto-generated />
using System;
using Kolokwium_apbd_s20414.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Kolokwium_apbd_s20414.Migrations
{
    [DbContext(typeof(KolokwiumContext))]
    [Migration("20230604140235_Test")]
    partial class Test
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Client", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ID");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            FirstName = "John",
                            LastName = "Doe"
                        },
                        new
                        {
                            ID = 2,
                            FirstName = "Jane",
                            LastName = "Smith"
                        });
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("Client_ID")
                        .HasColumnType("int");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FulfilledAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("Status_ID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("Client_ID");

                    b.HasIndex("Status_ID");

                    b.ToTable("Orders");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Client_ID = 1,
                            CreatedAt = new DateTime(2023, 6, 4, 16, 2, 35, 399, DateTimeKind.Local).AddTicks(2151),
                            Status_ID = 1
                        },
                        new
                        {
                            ID = 2,
                            Client_ID = 2,
                            CreatedAt = new DateTime(2023, 6, 4, 16, 2, 35, 399, DateTimeKind.Local).AddTicks(2193),
                            FulfilledAt = new DateTime(2023, 6, 4, 16, 2, 35, 399, DateTimeKind.Local).AddTicks(2195),
                            Status_ID = 2
                        });
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Product", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("ID");

                    b.ToTable("Products");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Product 1",
                            Price = 9.9900000000000002
                        },
                        new
                        {
                            ID = 2,
                            Name = "Product 2",
                            Price = 19.989999999999998
                        });
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.ProductOrder", b =>
                {
                    b.Property<int>("Product_ID")
                        .HasColumnType("int");

                    b.Property<int>("Order_ID")
                        .HasColumnType("int");

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.HasKey("Product_ID", "Order_ID");

                    b.HasIndex("Order_ID");

                    b.ToTable("ProductOrders");

                    b.HasData(
                        new
                        {
                            Product_ID = 1,
                            Order_ID = 1,
                            Amount = 2
                        },
                        new
                        {
                            Product_ID = 2,
                            Order_ID = 1,
                            Amount = 1
                        },
                        new
                        {
                            Product_ID = 1,
                            Order_ID = 2,
                            Amount = 3
                        });
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Status", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("ID");

                    b.ToTable("Statuses");

                    b.HasData(
                        new
                        {
                            ID = 1,
                            Name = "Created"
                        },
                        new
                        {
                            ID = 2,
                            Name = "Created"
                        });
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Order", b =>
                {
                    b.HasOne("Kolokwium_apbd_s20414.Models.Client", "Client")
                        .WithMany("Orders")
                        .HasForeignKey("Client_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kolokwium_apbd_s20414.Models.Status", "Status")
                        .WithMany("Orders")
                        .HasForeignKey("Status_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");

                    b.Navigation("Status");
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.ProductOrder", b =>
                {
                    b.HasOne("Kolokwium_apbd_s20414.Models.Order", "Order")
                        .WithMany("ProductOrders")
                        .HasForeignKey("Order_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Kolokwium_apbd_s20414.Models.Product", "Product")
                        .WithMany("ProductOrders")
                        .HasForeignKey("Product_ID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Client", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Order", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Product", b =>
                {
                    b.Navigation("ProductOrders");
                });

            modelBuilder.Entity("Kolokwium_apbd_s20414.Models.Status", b =>
                {
                    b.Navigation("Orders");
                });
#pragma warning restore 612, 618
        }
    }
}
