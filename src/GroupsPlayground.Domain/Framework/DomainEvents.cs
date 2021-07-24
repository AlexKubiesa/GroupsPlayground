using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GroupsPlayground.Domain.Framework
{
    public static class DomainEvents
    {
        private static readonly List<Type> StaticHandlers;

        static DomainEvents()
        {
            StaticHandlers = new List<Type>();

            Register(Assembly.GetExecutingAssembly());
        }

        public static void Register(Assembly assembly)
        {
            StaticHandlers.AddRange(assembly.GetTypes()
                .Where(x => x.GetInterfaces()
                    .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>))));
        }

        public static void Raise<T>(T @event)
            where T : IDomainEvent
        {
            foreach (var type in StaticHandlers.Where(typeof(IHandler<T>).IsAssignableFrom))
            {
                var handler = (IHandler<T>)Activator.CreateInstance(type);
                handler.Handle(@event);
            }
        }

        public static void DispatchEvents(AggregateRoot aggregateRoot)
        {
            foreach (var @event in aggregateRoot.DomainEvents)
            {
                Dispatch(@event);
            }

            aggregateRoot.ClearDomainEvents();
        }

        private static void Dispatch(IDomainEvent @event)
        {
            foreach (object handler in
                from type in StaticHandlers
                let canHandleEvent =
                    type.GetInterfaces()
                    .Any(x => x.IsGenericType
                              && x.GetGenericTypeDefinition() == typeof(IHandler<>)
                              && x.GenericTypeArguments[0] == @event.GetType())
                where canHandleEvent
                select Activator.CreateInstance(type))
            {
                ((dynamic) handler).Handle((dynamic) @event);
            }
        }
    }
}