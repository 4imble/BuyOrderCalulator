using Microsoft.EntityFrameworkCore;
using Swarm.Domain;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Swarm.EntityFramework
{
    public class DataContext : DbContext
    {
        protected DataContext() { }

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
        }

        public DbSet<Game> Games { get; set; }

    }
}
