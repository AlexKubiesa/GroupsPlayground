using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GroupsPlayground.Domain.Framework
{
    public static class DomainEvents
    {
        private static readonly List<Type> StaticHandlers;
        private static readonly Dictionary<Type, List<Delegate>> DynamicHandlers;

        static DomainEvents()
        {
            StaticHandlers = new List<Type>();

            Register(Assembly.GetExecutingAssembly());

            DynamicHandlers = Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(x => typeof(IDomainEvent).IsAssignableFrom(x) && !x.IsInterface)
                .ToDictionary(x => x, x => new List<Delegate>());
        }

        public static void Register(Assembly assembly)
        {
            StaticHandlers.AddRange(assembly.GetTypes()
                .Where(x => x.GetInterfaces()
                    .Any(y => y.IsGenericType && y.GetGenericTypeDefinition() == typeof(IHandler<>))));
        }

        public static void Register<T>(Action<T> eventHandler)
            where T : IDomainEvent
        {
            DynamicHandlers[typeof(T)].Add(eventHandler);
        }

        public static void Raise<T>(T @event)
            where T : IDomainEvent
        {
            foreach (var handler in DynamicHandlers[@event.GetType()])
            {
                var action = (Action<T>)handler;
                action(@event);
            }

            foreach (var type in StaticHandlers.Where(typeof(IHandler<T>).IsAssignableFrom))
            {
                var handler = (IHandler<T>)Activator.CreateInstance(type);
                handler.Handle(@event);
            }
        }
    }
}