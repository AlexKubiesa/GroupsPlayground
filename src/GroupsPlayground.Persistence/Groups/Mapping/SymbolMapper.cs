using GroupsPlayground.Domain.Groups;

namespace GroupsPlayground.Persistence.Groups.Mapping
{
    public static class SymbolMapper
    {
        public static Symbol ToDomain(Model.Symbol symbol) => new Symbol(symbol.Text);

        public static Model.Symbol ToPersistence(Symbol symbol) => new Model.Symbol(symbol.Text);
    }
}