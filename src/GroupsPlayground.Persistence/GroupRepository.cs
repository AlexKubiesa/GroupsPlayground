using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GroupsPlayground.Domain;
using GroupsPlayground.Persistence.Mapping;
using Microsoft.EntityFrameworkCore;

namespace GroupsPlayground.Persistence
{
    public sealed class GroupRepository
    {
        private readonly GroupsPlaygroundContext context;

        internal GroupRepository(GroupsPlaygroundContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Group GetGroup(Guid id) => GroupMapper.ToDomain(context.Groups.Find(id));

        public List<Group> GetAllGroups()
        {
            var groups = context.Groups.ToList();
            return groups.Select(GroupMapper.ToDomain).ToList();
        }

        public void AddGroup(Group group) => context.Groups.Add(GroupMapper.ToPersistence(group));

        public async Task AddGroupAsync(Group group) => await context.Groups.AddAsync(GroupMapper.ToPersistence(group));

        public void RemoveGroup(Group group) => context.Groups.Remove(context.Groups.Find(group.Id));
    }
}