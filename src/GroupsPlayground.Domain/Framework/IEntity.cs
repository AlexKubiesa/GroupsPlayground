using System;

namespace GroupsPlayground.Domain.Framework
{
    public interface IEntity
    {
        Guid Id { get; }
        bool Equals(object obj);
        int GetHashCode();
    }
}