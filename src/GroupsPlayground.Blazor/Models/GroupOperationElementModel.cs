using GroupsPlayground.Domain;

namespace GroupsPlayground.Blazor.Models
{
    public class GroupOperationElementModel
    {
        public GroupOperationElementModel(Symbol symbol) => Symbol = symbol?.ToString();

        public string Symbol { get; }
    }
}