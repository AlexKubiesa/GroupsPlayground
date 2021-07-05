namespace GroupsPlayground.Persistence.Mapping
{
    public static class SymbolMapper
    {
        public static Domain.Symbol ToDomain(Model.Symbol symbol) => new Domain.Symbol(symbol.Text);

        public static Model.Symbol ToPersistence(Domain.Symbol symbol) => new Model.Symbol(symbol.Text);
    }
}