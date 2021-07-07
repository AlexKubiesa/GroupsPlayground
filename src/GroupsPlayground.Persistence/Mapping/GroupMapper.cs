using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Mapping
{
    public static class GroupMapper
    {
        public static Domain.Group ToDomain(Model.Group group)
        {
            var symbols =
                new Domain.GroupElementSymbols(group.Elements.Select(x => SymbolMapper.ToDomain(x.Symbol)).ToValueList());

            var cayleyTable = new Domain.CayleyTable(Guid.NewGuid(), symbols);
            foreach (var product in group.Products)
            {
                int row = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.First.Symbol));
                int column = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.Second.Symbol));
                cayleyTable.Products[row][column] = SymbolMapper.ToDomain(product.Product.Symbol);
            }

            return new Domain.Group(group.Id, group.Name, cayleyTable);
        }

        public static Model.Group ToPersistence(Domain.Group group)
        {
            var elements = group.Elements.ToDictionary(x => x, ToPersistence);
            var products = new List<Model.GroupElementProduct>(group.Elements.Count * group.Elements.Count);
            products.AddRange(
                from first in elements.Keys
                from second in elements.Keys
                select new Model.GroupElementProduct(Guid.NewGuid(), elements[first], elements[second],
                    elements[@group.Multiply(first, second)]));

            return new Model.Group(group.Id)
            {
                Name = group.Name,
                Elements = elements.Values.ToArray(),
                Products = products
            };
        }

        private static Model.GroupElement ToPersistence(Domain.IGroupElement element) =>
            new Model.GroupElement(element.Id, SymbolMapper.ToPersistence(element.Symbol));
    }
}