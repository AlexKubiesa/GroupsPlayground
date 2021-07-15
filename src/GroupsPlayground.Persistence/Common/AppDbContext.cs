using Microsoft.EntityFrameworkCore;

namespace GroupsPlayground.Persistence.Common
{
    internal abstract class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
            optionsBuilder.UseSqlServer(
                @"Server=(LocalDb)\GroupsPlayground; Integrated Security=true; Database=GroupsPlayground; AttachDbFilename=C:\Temp\GroupsPlayground.mdf");
    }
}