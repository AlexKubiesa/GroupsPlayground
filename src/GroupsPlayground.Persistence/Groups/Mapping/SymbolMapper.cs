using DomainModel = GroupsPlayground.Domain.Groups;
using PersistenceModel = GroupsPlayground.Persistence.Groups.Model;

namespace GroupsPlayground.Persistence.Groups.Mapping
{
    public static class SymbolMapper
    {
        public static DomainModel.Symbol ToDomain(PersistenceModel.Symbol symbol) =>
            new DomainModel.Symbol(symbol.Text);

        public static PersistenceModel.Symbol ToPersistence(DomainModel.Symbol symbol) =>
            new PersistenceModel.Symbol(symbol.Text);
    }
}