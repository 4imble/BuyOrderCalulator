﻿// <auto-generated />
using System;
using BuyOrderCalc.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuyOrderCalc.EntityFramework.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20200910203338_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6");

            modelBuilder.Entity("BuyOrderCalc.Domain.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("Quantity")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReorderCreditValue")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ReorderLevel")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("TakingOrders")
                        .HasColumnType("INTEGER");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UnitPrice")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Tritanium",
                            Quantity = 17450000,
                            ReorderCreditValue = 1,
                            ReorderLevel = 18742950,
                            TakingOrders = true,
                            TypeId = 1,
                            UnitPrice = 2
                        },
                        new
                        {
                            Id = 2,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Pyerite",
                            Quantity = 6950000,
                            ReorderCreditValue = 1,
                            ReorderLevel = 5312610,
                            TakingOrders = true,
                            TypeId = 1,
                            UnitPrice = 18
                        },
                        new
                        {
                            Id = 3,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Mexallon",
                            Quantity = 2290000,
                            ReorderCreditValue = 1,
                            ReorderLevel = 1665210,
                            TakingOrders = true,
                            TypeId = 1,
                            UnitPrice = 32
                        },
                        new
                        {
                            Id = 4,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Isogen",
                            Quantity = 170000,
                            ReorderCreditValue = 1,
                            ReorderLevel = 284720,
                            TakingOrders = true,
                            TypeId = 1,
                            UnitPrice = 110
                        },
                        new
                        {
                            Id = 5,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Nocxium",
                            Quantity = 130000,
                            ReorderCreditValue = 1,
                            ReorderLevel = 67810,
                            TakingOrders = true,
                            TypeId = 1,
                            UnitPrice = 1000
                        });
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.ItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<Guid>("Guid")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("ItemTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Mineral"
                        },
                        new
                        {
                            Id = 2,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Planetary"
                        },
                        new
                        {
                            Id = 3,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Debt"
                        },
                        new
                        {
                            Id = 4,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Wallet"
                        });
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.Item", b =>
                {
                    b.HasOne("BuyOrderCalc.Domain.ItemType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
