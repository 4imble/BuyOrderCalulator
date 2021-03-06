﻿using Microsoft.EntityFrameworkCore;
using BuyOrderCalc.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Collections.Generic;

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
                new ItemType { Id = 1, Name = "Unclassified" },
                new ItemType { Id = 2, Name = "Mineral" },
                new ItemType { Id = 3, Name = "Planetary" },
                new ItemType { Id = 4, Name = "Debt" },
                new ItemType { Id = 5, Name = "Wallet" }
                );

            builder.Entity<SupplyType>().HasData(
                new SupplyType { Id = 1, Name = "High", PricePercentModifier = 80, CorpCreditPercent = 2 },
                new SupplyType { Id = 2, Name = "Low", PricePercentModifier = 90, CorpCreditPercent = 3 },
                new SupplyType { Id = 3, Name = "Emergency", PricePercentModifier = 105, CorpCreditPercent = 4 },
                new SupplyType { Id = 4, Name = "Unwanted", PricePercentModifier = 1, CorpCreditPercent = 0 },
                new SupplyType { Id = 5, Name = "Misc Ore", PricePercentModifier = 95, CorpCreditPercent = 3 }
            );

            builder.Entity<RefinementSkill>().HasData(
                new RefinementSkill { Id = 1, Quality = OreQuality.Common, Efficiency = 60 },
                new RefinementSkill { Id = 2, Quality = OreQuality.Uncommon, Efficiency = 60 },
                new RefinementSkill { Id = 3, Quality = OreQuality.Special, Efficiency = 60 },
                new RefinementSkill { Id = 4, Quality = OreQuality.Rare, Efficiency = 60 },
                new RefinementSkill { Id = 5, Quality = OreQuality.Precious, Efficiency = 52.5 }
            );
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<ItemType> ItemTypes { get; set; }
        public DbSet<SupplyType> SupplyTypes { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefinementSkill> RefinementSkills { get; set; }

    }
}
