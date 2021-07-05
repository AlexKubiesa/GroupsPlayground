using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public interface IGroupElement : IEntity
    {
        Symbol Symbol { get; }
    }
}