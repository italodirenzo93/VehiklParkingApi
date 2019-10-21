﻿// <auto-generated />

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using ParkingLot.Data.Models;

namespace ParkingLot.Data.Migrations
{
    [DbContext(typeof(ParkingLotDbContext))]
    [Migration("20190704041907_ConvertTimeToUtc")]
    partial class ConvertTimeToUtc
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062");

            modelBuilder.Entity("ParkingLot.Api.Models.RateLevel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double?>("Duration");

                    b.Property<string>("Name");

                    b.Property<decimal>("RateValue");

                    b.HasKey("Id");

                    b.ToTable("RateLevels");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Duration = 3600000.0,
                            Name = "1hr",
                            RateValue = 3.00m
                        },
                        new
                        {
                            Id = 2,
                            Duration = 10800000.0,
                            Name = "3hr",
                            RateValue = 4.50m
                        },
                        new
                        {
                            Id = 3,
                            Duration = 21600000.0,
                            Name = "6hr",
                            RateValue = 6.75m
                        },
                        new
                        {
                            Id = 4,
                            Name = "ALL DAY",
                            RateValue = 10.125m
                        });
                });

            modelBuilder.Entity("ParkingLot.Api.Models.Ticket", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Customer");

                    b.Property<DateTimeOffset>("IssuedOn");

                    b.Property<int>("RateLevelId");

                    b.HasKey("Id");

                    b.HasIndex("RateLevelId");

                    b.ToTable("Tickets");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Customer = "Italo Di Renzo",
                            IssuedOn = new DateTimeOffset(new DateTime(2019, 7, 3, 18, 19, 7, 38, DateTimeKind.Unspecified).AddTicks(9955), new TimeSpan(0, 0, 0, 0, 0)),
                            RateLevelId = 3
                        },
                        new
                        {
                            Id = 2,
                            Customer = "Tim Berners-Lee",
                            IssuedOn = new DateTimeOffset(new DateTime(2019, 7, 4, 0, 19, 7, 38, DateTimeKind.Unspecified).AddTicks(9955), new TimeSpan(0, 0, 0, 0, 0)),
                            RateLevelId = 1
                        },
                        new
                        {
                            Id = 3,
                            Customer = "Leon S. Kennedy",
                            IssuedOn = new DateTimeOffset(new DateTime(2019, 7, 3, 15, 19, 7, 38, DateTimeKind.Unspecified).AddTicks(9955), new TimeSpan(0, 0, 0, 0, 0)),
                            RateLevelId = 4
                        },
                        new
                        {
                            Id = 4,
                            Customer = "Gordon Freeman",
                            IssuedOn = new DateTimeOffset(new DateTime(2019, 7, 4, 2, 19, 7, 38, DateTimeKind.Unspecified).AddTicks(9955), new TimeSpan(0, 0, 0, 0, 0)),
                            RateLevelId = 1
                        });
                });

            modelBuilder.Entity("ParkingLot.Api.Models.Ticket", b =>
                {
                    b.HasOne("ParkingLot.Api.Models.RateLevel", "RateLevel")
                        .WithMany()
                        .HasForeignKey("RateLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
