using System;

namespace GroupsPlayground.Persistence.Framework
{
    public abstract class Entity
    {
        protected Entity(Guid id) => Id = id;

        public virtual Guid Id { get; private set; }
    }
}
