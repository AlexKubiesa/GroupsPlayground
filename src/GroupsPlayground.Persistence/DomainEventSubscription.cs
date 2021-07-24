using System.Reflection;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence
{
    public static class DomainEventSubscription
    {
        public static void Initialise() => DomainEvents.Register(Assembly.GetExecutingAssembly());
    }
}