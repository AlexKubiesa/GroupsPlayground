using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Groups;
using GroupsPlayground.Persistence.Common;
using GroupsPlayground.Persistence.Groups.Mapping;

namespace GroupsPlayground.Persistence.Groups
{
    public sealed class GroupRepository
    {
        private readonly AppDbContext context;

        internal GroupRepository(AppDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Group GetGroup(Guid id) =>
            GroupMapper.ToDomain(context.Groups.Find(id));

        public List<Group> GetAllGroups()
        {
            var groups = context.Groups.ToList();
            return groups.Select(GroupMapper.ToDomain).ToList();
        }

        public void AddGroup(Group group)
        {
            context.Groups.Add(GroupMapper.ToPersistence(@group));
            DomainEvents.Raise(new GroupAddedEvent());
        }

        public async Task AddGroupAsync(Group group)
        {
            await context.Groups.AddAsync(GroupMapper.ToPersistence(group));
            DomainEvents.Raise(new GroupAddedEvent());
        }

        public void RemoveGroup(Group group) => context.Groups.Remove(context.Groups.Find(group.Id));
    }
}