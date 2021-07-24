using System.Collections.Generic;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Persistence.Framework;
using GroupsPlayground.Persistence.Groups;
using GroupsPlayground.Persistence.Groups.Model;
using GroupsPlayground.Persistence.Quests;
using GroupsPlayground.Persistence.Quests.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GroupsPlayground.Persistence.Common
{
    internal class AppDbContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<CayleyTableGroup> CayleyTableGroups { get; set; }
        public DbSet<PermutationGroup> PermutationGroups { get; set; }

        public DbSet<Quest> Quests { get; set; }

        public void AddReferenceData()
        {
            Quests.AddRange(ReferenceData.Quests);
        }

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

            modelBuilder.Entity<PermutationGroup>().Property(x => x.Size);
            modelBuilder.Entity<PermutationGroup>().Property(x => x.Generators).HasJsonConversion();

            modelBuilder.Entity<Quest>().HasKey(x => x.Id);
            modelBuilder.Entity<Quest>().Property(x => x.Complete);
        }
    }
}