namespace GroupsPlayground.Domain.Framework
{
    public interface IHandler<T>
        where T : IDomainEvent
    {
        public void Handle(T @event);
    }
}