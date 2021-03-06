﻿// <auto-generated />
using System;
using BuyOrderCalc.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BuyOrderCalc.EntityFramework.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20201015110341_refining skills update")]
    partial class refiningskillsupdate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BuyOrderCalc.Domain.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ApiId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<double>("MarketPrice")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<int>("ReorderLevel")
                        .HasColumnType("int");

                    b.Property<int>("SupplyTypeId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("SupplyTypeId");

                    b.HasIndex("TypeId");

                    b.ToTable("Items");
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.ItemType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ItemTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Unclassified"
                        },
                        new
                        {
                            Id = 2,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Mineral"
                        },
                        new
                        {
                            Id = 3,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Planetary"
                        },
                        new
                        {
                            Id = 4,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Debt"
                        },
                        new
                        {
                            Id = 5,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Wallet"
                        });
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("DateCreated")
                        .HasColumnType("datetime2");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.Property<int?>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("FixedCorpCreditPercent")
                        .HasColumnType("float");

                    b.Property<double>("FixedUnitPrice")
                        .HasColumnType("float");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int?>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ItemId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItem");
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.RefinementSkill", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Efficiency")
                        .HasColumnType("float");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quality")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("RefinementSkills");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Efficiency = 60.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Quality = 0
                        },
                        new
                        {
                            Id = 2,
                            Efficiency = 60.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Quality = 1
                        },
                        new
                        {
                            Id = 3,
                            Efficiency = 60.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Quality = 2
                        },
                        new
                        {
                            Id = 4,
                            Efficiency = 60.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Quality = 3
                        },
                        new
                        {
                            Id = 5,
                            Efficiency = 52.5,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Quality = 4
                        });
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.SupplyType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("CorpCreditPercent")
                        .HasColumnType("float");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PricePercentModifier")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SupplyTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CorpCreditPercent = 2.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "High",
                            PricePercentModifier = 80
                        },
                        new
                        {
                            Id = 2,
                            CorpCreditPercent = 3.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Low",
                            PricePercentModifier = 90
                        },
                        new
                        {
                            Id = 3,
                            CorpCreditPercent = 4.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Emergency",
                            PricePercentModifier = 105
                        },
                        new
                        {
                            Id = 4,
                            CorpCreditPercent = 0.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Unwanted",
                            PricePercentModifier = 1
                        },
                        new
                        {
                            Id = 5,
                            CorpCreditPercent = 3.0,
                            Guid = new Guid("00000000-0000-0000-0000-000000000000"),
                            Name = "Misc Ore",
                            PricePercentModifier = 95
                        });
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccessToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DiscordId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("Guid")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<DateTime>("TokenExpires")
                        .HasColumnType("datetime2");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.Item", b =>
                {
                    b.HasOne("BuyOrderCalc.Domain.SupplyType", "SupplyType")
                        .WithMany()
                        .HasForeignKey("SupplyTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuyOrderCalc.Domain.ItemType", "Type")
                        .WithMany()
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.Order", b =>
                {
                    b.HasOne("BuyOrderCalc.Domain.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("BuyOrderCalc.Domain.OrderItem", b =>
                {
                    b.HasOne("BuyOrderCalc.Domain.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BuyOrderCalc.Domain.Order", null)
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId");
                });
#pragma warning restore 612, 618
        }
    }
}
