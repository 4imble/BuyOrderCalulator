using Microsoft.EntityFrameworkCore;
using BuyOrderCalc.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace BuyOrderCalc.EntityFramework
{
    public class DataContext : DbContext
    {
        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options) { }

        public override int SaveChanges()
        {
            var validationErrors = ChangeTracker
                .Entries<IValidatableObject>()
                .SelectMany(e => e.Entity.Validate(null))
                .Where(r => r != ValidationResult.Success);

            if (validationErrors.Any())
            {
                throw new Exception("Save changes error");
            }

            return base.SaveChanges();
        }



        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ItemType>().HasData(
                new ItemType { Id = 1, Name = "Mineral" },
                new ItemType { Id = 2, Name = "Planetary" },
                new ItemType { Id = 3, Name = "Debt" },
                new ItemType { Id = 4, Name = "Wallet" }
                );

            builder.Entity<SupplyType>().HasData(
                new SupplyType { Id = 1, Name = "High", PricePercentModifier = 80, CorpCreditPercent = 2 },
                new SupplyType { Id = 2, Name = "Low", PricePercentModifier = 90, CorpCreditPercent = 3 },
                new SupplyType { Id = 3, Name = "Emergency", PricePercentModifier = 105, CorpCreditPercent = 4 },
                new SupplyType { Id = 4, Name = "Unwanted", PricePercentModifier = 1, CorpCreditPercent = 0 }
            );

            builder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Tritanium", TypeId = 1, MarketPrice = 2, Quantity = 17450000, ReorderLevel = 18742950, SupplyTypeId = 1 },
                new Item { Id = 2, Name = "Pyerite", TypeId = 1, MarketPrice = 18, Quantity = 6950000, ReorderLevel = 5312610, SupplyTypeId = 2 },
                new Item { Id = 3, Name = "Mexallon", TypeId = 1, MarketPrice = 32, Quantity = 2290000, ReorderLevel = 1665210, SupplyTypeId = 3 },
                new Item { Id = 4, Name = "Isogen", TypeId = 1, MarketPrice = 110, Quantity = 170000, ReorderLevel = 284720, SupplyTypeId = 3 },
                new Item { Id = 5, Name = "Nocxium", TypeId = 1, MarketPrice = 1000, Quantity = 130000, ReorderLevel = 67810, SupplyTypeId = 2 },
                new Item { Id = 6, Name = "Zydrine", TypeId = 1, MarketPrice = 225, Quantity = 5413, ReorderLevel = 0, SupplyTypeId = 4 }
                );
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<Order> Orders { get; set; }

    }
}
