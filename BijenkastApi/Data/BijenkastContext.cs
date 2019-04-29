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
            builder.Entity<Bijenkast>().Property(r => r.Id).ValueGeneratedOnAdd();
            builder.Entity<Bijenkast>().HasKey(r => r.Id);

            builder.Entity<Imker>().Property(r => r.ImkerId).ValueGeneratedOnAdd();
            builder.Entity<Imker>().HasKey(r => r.ImkerId);

            builder.Entity<Imker>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Imker>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Imker>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Imker>().HasMany(c => c.bijenkasten).WithOne();
            
        }

        public DbSet<Bijenkast> Bijenkasten { get; set; }
        public DbSet<Imker> Imkers { get; set; }
    }
}