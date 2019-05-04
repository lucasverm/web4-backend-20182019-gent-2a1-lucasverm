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
            builder.Entity<Bijenkast>().Property(r => r.naam).IsRequired().HasMaxLength(50);
            builder.Entity<Bijenkast>().Property(r => r.id).ValueGeneratedOnAdd();
            builder.Entity<Bijenkast>().HasKey(r => r.id);
            builder.Entity<Bijenkast>().HasMany(c => c.inspecties).WithOne();

            builder.Entity<Imker>().Property(r => r.ImkerId).ValueGeneratedOnAdd();
            builder.Entity<Imker>().HasKey(r => r.ImkerId);

            builder.Entity<Imker>().Property(c => c.LastName).IsRequired().HasMaxLength(50);
            builder.Entity<Imker>().Property(c => c.FirstName).IsRequired().HasMaxLength(50);
            builder.Entity<Imker>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Imker>().HasMany(c => c.bijenkasten).WithOne();

            builder.Entity<Inspectie>().Property(r => r.id).ValueGeneratedOnAdd();
            builder.Entity<Inspectie>().HasKey(r => r.id);
        }

        public DbSet<Inspectie> Inspecties { get; set; }
        public DbSet<Bijenkast> Bijenkasten { get; set; }
        public DbSet<Imker> Imkers { get; set; }
    }
}