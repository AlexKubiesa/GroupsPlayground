using System;
using Microsoft.EntityFrameworkCore;

namespace GroupsPlayground.Persistence
{
    public class Session : IDisposable
    {
        private readonly GroupsPlaygroundContext context = new GroupsPlaygroundContext();

        public Session()
        {
            GroupRepository = new GroupRepository(context);
        }

        public GroupRepository GroupRepository { get; }

        public void CreateDatabase() => context.Database.EnsureCreated();

        public void Dispose() => context?.Dispose();
    }
}