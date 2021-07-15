using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Groups
{
    public interface IGroupElement : IEntity
    {
        Symbol Symbol { get; }
    }
}