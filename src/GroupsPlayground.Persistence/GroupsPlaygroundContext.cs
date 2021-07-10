﻿using System;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Persistence.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace GroupsPlayground.Persistence
{
    internal sealed class GroupsPlaygroundContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<CayleyTableGroup> CayleyTableGroups { get; set; }
        public DbSet<PermutationGroup> PermutationGroups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDb)\GroupsPlayground; Integrated Security=true; Database=GroupsPlayground; AttachDbFilename=C:\Temp\GroupsPlayground.mdf");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseValueConverter(new SymbolToStringConverter());

            modelBuilder.Entity<Group>().HasKey(x => x.Id);
            modelBuilder.Entity<Group>().Property(x => x.Name);

            modelBuilder.Entity<CayleyTableGroup>().HasMany(x => x.Elements).WithOne();
            modelBuilder.Entity<CayleyTableGroup>().Navigation(x => x.Elements).AutoInclude();
            modelBuilder.Entity<CayleyTableGroup>().HasMany(x => x.Products).WithOne();
            modelBuilder.Entity<CayleyTableGroup>().Navigation(x => x.Products).AutoInclude();

            modelBuilder.Entity<GroupElement>().HasKey(x => x.Id);
            modelBuilder.Entity<GroupElement>().Property(x => x.Symbol);

            modelBuilder.Entity<GroupElementProduct>().HasKey(x => x.Id);
            modelBuilder.Entity<GroupElementProduct>().HasOne(x => x.First).WithMany();
            modelBuilder.Entity<GroupElementProduct>().HasOne(x => x.Second).WithMany();
            modelBuilder.Entity<GroupElementProduct>().HasOne(x => x.Product).WithMany();

            modelBuilder.Entity<PermutationGroup>().HasMany<Permutation>().WithOne();

            modelBuilder.Entity<Permutation>().HasKey(x => x.Id);
            modelBuilder.Entity<Permutation>().Property(x => x.Expression);
        }
    }
}
