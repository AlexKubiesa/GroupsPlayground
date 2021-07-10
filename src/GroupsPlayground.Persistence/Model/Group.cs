using System;

namespace GroupsPlayground.Persistence.Model
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