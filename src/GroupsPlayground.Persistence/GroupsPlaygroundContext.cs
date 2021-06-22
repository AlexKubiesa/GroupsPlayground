using System;
using GroupsPlayground.Domain;
using GroupsPlayground.Domain.Framework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GroupsPlayground.Persistence
{
    internal sealed class GroupsPlaygroundContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDb)\GroupsPlayground; Integrated Security=true; Database=GroupsPlayground; AttachDbFilename=C:\Temp\GroupsPlayground.mdf");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseValueConverter(new SymbolToStringConverter());

            modelBuilder.Entity<Group>().HasKey(x => x.Id);
            modelBuilder.Entity<Group>().HasMany(group => group.Elements).WithOne();
            modelBuilder.Entity<Group>().HasMany(group => group.Products).WithOne();

            modelBuilder.Entity<GroupElement>().HasKey(x => x.Id);
            modelBuilder.Entity<GroupElement>().Property(x => x.Symbol);

            modelBuilder.Entity<GroupElementProduct>().HasKey(x => x.Id);
            modelBuilder.Entity<GroupElementProduct>().HasOne(x => x.First).WithMany();
            modelBuilder.Entity<GroupElementProduct>().HasOne(x => x.Second).WithMany();
            modelBuilder.Entity<GroupElementProduct>().HasOne(x => x.Product).WithMany();
        }
    }
}
