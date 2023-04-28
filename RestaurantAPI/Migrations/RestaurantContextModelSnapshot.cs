﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RestaurantAPI.Contexts;

#nullable disable

namespace RestaurantAPI.Migrations
{
    [DbContext(typeof(RestaurantContext))]
    partial class RestaurantContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("RestaurantAPI.Entities.Category", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.HasKey("ID");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("RestaurantAPI.Entities.Menu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<int>("CategoryID")
                        .HasColumnType("int");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("MenuName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Price")
                        .HasColumnType("real");

                    b.HasKey("ID");

                    b.HasIndex("CategoryID")
                        .IsUnique();

                    b.ToTable("Menus", (string)null);
                });

            modelBuilder.Entity("RestaurantAPI.Entities.Order", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Orders", (string)null);
                });

            modelBuilder.Entity("RestaurantAPI.Entities.OrderMenu", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<int>("MenuID")
                        .HasColumnType("int");

                    b.Property<int>("OrderID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("MenuID");

                    b.HasIndex("OrderID");

                    b.ToTable("OrderMenus", (string)null);
                });

            modelBuilder.Entity("RestaurantAPI.Entities.User", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"));

                    b.Property<bool?>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("RestaurantAPI.Entities.Menu", b =>
                {
                    b.HasOne("RestaurantAPI.Entities.Category", "Category")
                        .WithOne("Menu")
                        .HasForeignKey("RestaurantAPI.Entities.Menu", "CategoryID")
                        .IsRequired()
                        .HasConstraintName("FK_Menus_Category");

                    b.Navigation("Category");
                });

            modelBuilder.Entity("RestaurantAPI.Entities.Order", b =>
                {
                    b.HasOne("RestaurantAPI.Entities.User", "User")
                        .WithMany("Order")
                        .HasForeignKey("UserID")
                        .IsRequired()
                        .HasConstraintName("FK_Users_Orders");

                    b.Navigation("User");
                });

            modelBuilder.Entity("RestaurantAPI.Entities.OrderMenu", b =>
                {
                    b.HasOne("RestaurantAPI.Entities.Menu", "Menu")
                        .WithMany("OrderMenu")
                        .HasForeignKey("MenuID")
                        .IsRequired()
                        .HasConstraintName("FK_Menus_OrderMenus");

                    b.HasOne("RestaurantAPI.Entities.Order", "Order")
                        .WithMany("OrderMenu")
                        .HasForeignKey("OrderID")
                        .IsRequired()
                        .HasConstraintName("FK_Orders_OrderMenus");

                    b.Navigation("Menu");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("RestaurantAPI.Entities.Category", b =>
                {
                    b.Navigation("Menu")
                        .IsRequired();
                });

            modelBuilder.Entity("RestaurantAPI.Entities.Menu", b =>
                {
                    b.Navigation("OrderMenu");
                });

            modelBuilder.Entity("RestaurantAPI.Entities.Order", b =>
                {
                    b.Navigation("OrderMenu");
                });

            modelBuilder.Entity("RestaurantAPI.Entities.User", b =>
                {
                    b.Navigation("Order");
                });
#pragma warning restore 612, 618
        }
    }
}