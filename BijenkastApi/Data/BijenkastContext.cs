using BijenkastApi.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BijenkastApi.Data
{
    public class BijenkastContext : DbContext
    {
        public BijenkastContext(DbContextOptions<BijenkastContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            /*builder.Entity<Bijenkast>()
                .HasOne(p => p.moer);
            //.WithOne()
            //.IsRequired();
            // .HasForeignKey("moer"); //Shadow property*/
            builder.Entity<Bijenkast>().Property(r => r.Name).IsRequired().HasMaxLength(50);

            //Another way to seed the database

            builder.Entity<Moer>().HasData(
                    //Shadow property can be used for the foreign key, in combination with anaonymous objects
                    new Moer { Id = 1, Name = "Queenb", gemerkt = true, geknipt = true, Geboortedag = DateTime.Today }

                 );

            builder.Entity<Bijenkast>().HasData(
                new Bijenkast { Id = 1, Name = "Kast1", Created = DateTime.Now },
                 new Bijenkast { Id = 2, Name = "Kast2", Created = DateTime.Now }
  );
        }

        public DbSet<Bijenkast> Bijenkasten { get; set; }
        public DbSet<Moer> Moeren { get; set; }
    }
}