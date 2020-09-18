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
                new SupplyType { Id = 1, Name = "High" },
                new SupplyType { Id = 2, Name = "Low" },
                new SupplyType { Id = 3, Name = "Emergency" },
                new SupplyType { Id = 4, Name = "Unwanted" }
            );

            builder.Entity<Item>().HasData(
                new Item { Id = 1, Name = "Tritanium", TypeId = 1, UnitPrice = 2, Quantity = 17450000, CorpCreditMultiplier = 1, ReorderLevel = 18742950, SupplyTypeId = 1 },
                new Item { Id = 2, Name = "Pyerite", TypeId = 1, UnitPrice = 18, Quantity = 6950000, CorpCreditMultiplier = 1, ReorderLevel = 5312610, SupplyTypeId = 2 },
                new Item { Id = 3, Name = "Mexallon", TypeId = 1, UnitPrice = 32, Quantity = 2290000, CorpCreditMultiplier = 1, ReorderLevel = 1665210, SupplyTypeId = 3 },
                new Item { Id = 4, Name = "Isogen", TypeId = 1, UnitPrice = 110, Quantity = 170000, CorpCreditMultiplier = 1, ReorderLevel = 284720, SupplyTypeId = 3 },
                new Item { Id = 5, Name = "Nocxium", TypeId = 1, UnitPrice = 1000, Quantity = 130000, CorpCreditMultiplier = 1, ReorderLevel = 67810, SupplyTypeId = 2 },
                new Item { Id = 6, Name = "Zydrine", TypeId = 1, UnitPrice = 225, Quantity = 5413, CorpCreditMultiplier = 1, ReorderLevel = 0, SupplyTypeId = 4 }
                );
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }

    }
}
