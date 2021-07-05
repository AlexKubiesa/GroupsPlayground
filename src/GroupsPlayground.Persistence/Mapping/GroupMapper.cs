using System;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Mapping
{
    public static class GroupMapper
    {
        public static Domain.Group ToDomain(Model.Group group)
        {
            var cayleyTable = new Domain.CayleyTable(Guid.NewGuid(), group.Elements.Count);
            foreach (var product in group.Products)
            {
                int row = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.First.Symbol));
                int column = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.Second.Symbol));
                cayleyTable.Products[row][column] = SymbolMapper.ToDomain(product.Product.Symbol);
            }

            return new Domain.Group(group.Id, group.Name, cayleyTable);
        }

        public static Model.Group ToPersistence(Domain.Group group) =>
            new Model.Group(group.Id)
            {
                Name = group.Name,
                Elements = group.Elements.Select(ToPersistence).ToArray(),
                Products = group.Products.Select(ToPersistence).ToArray()
            };

        private static Model.GroupElement ToPersistence(Domain.IGroupElement element) =>
            new Model.GroupElement(element.Id, SymbolMapper.ToPersistence(element.Symbol));

        private static Model.GroupElementProduct ToPersistence(Domain.GroupElementProduct product) =>
            new Model.GroupElementProduct(product.Id, ToPersistence(product.First), ToPersistence(product.Second),
                ToPersistence(product.Product));
    }
}