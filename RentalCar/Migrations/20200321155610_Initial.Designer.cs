﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RentalCar.Data;

namespace RentalCar.Migrations
{
    [DbContext(typeof(RentalCarContext))]
    [Migration("20200321155610_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RentalCar.Models.AutoModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Aviablity")
                        .HasColumnType("bit");

                    b.Property<int>("Capacity")
                        .HasColumnType("int");

                    b.Property<string>("CarMake")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("EngineCapacity")
                        .HasColumnType("float");

                    b.Property<string>("EngineType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FuelConsuption")
                        .HasColumnType("float");

                    b.Property<DateTime>("Issue")
                        .HasColumnType("datetime2");

                    b.Property<int>("Mileage")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Price")
                        .HasColumnType("int");

                    b.Property<string>("TransmissionType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Auto");
                });

            modelBuilder.Entity("RentalCar.Models.DriverModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("BirthdayDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("DistanceTraveled")
                        .HasColumnType("int");

                    b.Property<string>("DriveLisence")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DriverPicturePath")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Passport")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RentalJoinDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("RentalCar.Models.OrderModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AutoID")
                        .HasColumnType("int");

                    b.Property<int>("DriverID")
                        .HasColumnType("int");

                    b.Property<DateTime?>("OrderDayCount")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("OrderEndDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("OrderMilleage")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderStartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("TotalPrice")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("AutoID");

                    b.HasIndex("DriverID");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("RentalCar.Models.PictureModel", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("AutoModelID")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Path")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("AutoModelID");

                    b.ToTable("Pictures");
                });

            modelBuilder.Entity("RentalCar.Models.OrderModel", b =>
                {
                    b.HasOne("RentalCar.Models.AutoModel", "Auto")
                        .WithMany("Orders")
                        .HasForeignKey("AutoID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("RentalCar.Models.DriverModel", "Driver")
                        .WithMany("Orders")
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("RentalCar.Models.PictureModel", b =>
                {
                    b.HasOne("RentalCar.Models.AutoModel", null)
                        .WithMany("Pictures")
                        .HasForeignKey("AutoModelID");
                });
#pragma warning restore 612, 618
        }
    }
}
