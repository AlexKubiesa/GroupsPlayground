using GroupsPlayground.Persistence.Model;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GroupsPlayground.Persistence
{
    public class SymbolToStringConverter : ValueConverter<Symbol, string>
    {
        public SymbolToStringConverter(ConverterMappingHints mappingHints = null)
            : base(
                x => x.ToString(), 
                x => string.IsNullOrWhiteSpace(x) ? null : new Symbol(x),
                mappingHints)
        {
        }
    }
}