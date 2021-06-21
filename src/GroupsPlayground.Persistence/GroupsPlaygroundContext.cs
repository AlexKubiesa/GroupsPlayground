using System;
using GroupsPlayground.Domain;
using Microsoft.EntityFrameworkCore;

namespace GroupsPlayground.Persistence
{
    internal sealed class GroupsPlaygroundContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(@"Server=(LocalDb)\GroupsPlayground; Integrated Security=true; Database=GroupsPlayground; AttachDbFilename=C:\Temp\GroupsPlayground.mdf");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>().HasKey(x => x.Id);
        }
    }
}
