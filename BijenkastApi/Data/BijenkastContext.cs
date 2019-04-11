using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BijenkastApi.Models;
using System;

namespace BijenkastApi.Data
{
    public class BijenkastContext : IdentityDbContext
    {
        public BijenkastContext(DbContextOptions<BijenkastContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Bijenkast>().Property(r => r.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Imker>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Imker>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Imker>().Property(c => c.Email).IsRequired().HasMaxLength(100);

            //Another way to seed the database
            builder.Entity<Bijenkast>().HasData(
                new Bijenkast { Id = 1, Name = "Spaghetti", Created = DateTime.Now },
                new Bijenkast { Id = 2, Name = "Tomato soup", Created = DateTime.Now }
  );
        }

        public DbSet<Bijenkast> Bijenkasten { get; set; }
        public DbSet<Imker> Imkers { get; set; }
    }
}