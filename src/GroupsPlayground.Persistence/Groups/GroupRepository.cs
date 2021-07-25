using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Domain.Groups;
using GroupsPlayground.Persistence.Common;
using GroupsPlayground.Persistence.Framework;
using GroupsPlayground.Persistence.Groups.Mapping;

namespace GroupsPlayground.Persistence.Groups
{
    public sealed class GroupRepository : Repository
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
            OnAggregateAdded(group);
            OnDomainEventRaised(new GroupAddedEvent());
        }

        public async Task AddGroupAsync(Group group)
        {
            await context.Groups.AddAsync(GroupMapper.ToPersistence(group));
            OnAggregateAdded(group);
            OnDomainEventRaised(new GroupAddedEvent());
        }

        public void UpdateGroup(Group group)
        {
            context.Groups.Update(GroupMapper.ToPersistence(group));
            OnAggregateUpdated(group);
        }

        public void RemoveGroup(Group group)
        {
            context.Groups.Remove(context.Groups.Find(group.Id));
            OnAggregateRemoved(group);
        }
    }
}