using GroupsPlayground.Persistence.Common;
using GroupsPlayground.Persistence.Framework;
using GroupsPlayground.Persistence.Groups.Model;
using Microsoft.EntityFrameworkCore;

namespace GroupsPlayground.Persistence.Groups
{
    internal sealed class GroupsDbContext : AppDbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<CayleyTableGroup> CayleyTableGroups { get; set; }
        public DbSet<PermutationGroup> PermutationGroups { get; set; }

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

            modelBuilder.Entity<PermutationGroup>().HasMany(x => x.Generators).WithOne();
            modelBuilder.Entity<PermutationGroup>().Navigation(x => x.Generators).AutoInclude();

            modelBuilder.Entity<Permutation>().HasKey(x => x.Id);
            modelBuilder.Entity<Permutation>().Property(x => x.Expression);
        }
    }
}
