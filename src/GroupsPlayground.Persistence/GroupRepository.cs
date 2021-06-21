using System;
using GroupsPlayground.Domain;

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

        public void AddGroup(Group group)
        {
            context.Groups.Add(group);
            context.SaveChanges();
        }
    }
}