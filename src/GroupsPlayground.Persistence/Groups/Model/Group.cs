using System;
using GroupsPlayground.Persistence.Framework;

namespace GroupsPlayground.Persistence.Groups.Model
{
    public abstract class Group : Entity
    {
        protected Group(Guid id, string name) : base(id)
        {
            Name = name;
        }

        public string Name { get; }
    }
}