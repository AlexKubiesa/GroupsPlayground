using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain;
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

        public Group GetGroup(Guid id) => context.Groups.Find(id);

        public List<Group> GetAllGroups()
        {
            var groups = context.Groups.ToList();
            if (groups.Count > 0)
            {
                context.Entry(groups[0]).Collection(x => x.Elements).Load();
            }

            return groups;
        }

        public void AddGroup(Group group) => context.Groups.Add(group);

        public void RemoveGroup(Group group) => context.Groups.Remove(group);
    }
}