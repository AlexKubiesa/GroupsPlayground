using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Mapping
{
    public static class GroupMapper
    {
        public static Domain.Group ToDomain(Model.Group group) =>
            @group switch
            {
                Model.CayleyTableGroup cayleyTableGroup => ToDomain(cayleyTableGroup),
                Model.PermutationGroup permutationGroup => ToDomain(permutationGroup),
                _ => throw new ArgumentOutOfRangeException(nameof(@group))
            };

        public static Domain.CayleyTableGroup ToDomain(Model.CayleyTableGroup group)
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

            return new Domain.CayleyTableGroup(group.Id, group.Name, cayleyTable);
        }

        public static Domain.PermutationGroup ToDomain(Model.PermutationGroup group) =>
            new Domain.PermutationGroup(@group.Id, @group.Name, group.Generators.Select(PermutationMapper.ToDomain).ToValueList(), @group.Size);

        public static Model.Group ToPersistence(Domain.Group group) =>
            @group switch
            {
                Domain.CayleyTableGroup cayleyTableGroup => ToPersistence(cayleyTableGroup),
                Domain.PermutationGroup permutationGroup => ToPersistence(permutationGroup),
                _ => throw new ArgumentOutOfRangeException(nameof(@group))
            };

        public static Model.CayleyTableGroup ToPersistence(Domain.CayleyTableGroup group)
        {
            var elements = group.Elements.ToDictionary(x => x, ToPersistence);
            var products = new List<Model.GroupElementProduct>(group.Elements.Count * group.Elements.Count);
            products.AddRange(
                from first in elements.Keys
                from second in elements.Keys
                select new Model.GroupElementProduct(Guid.NewGuid(), elements[first], elements[second],
                    elements[@group.Multiply(first, second)]));

            return new Model.CayleyTableGroup(group.Id, group.Name)
            {
                Elements = elements.Values.ToArray(),
                Products = products
            };
        }

        public static Model.PermutationGroup ToPersistence(Domain.PermutationGroup group) =>
            new Model.PermutationGroup(@group.Id, group.Name)
            {
                Size = group.Size,
                Generators = group.Generators.Select(PermutationMapper.ToPersistence).ToArray()
            };

        private static Model.GroupElement ToPersistence(Domain.IGroupElement element) =>
            new Model.GroupElement(element.Id, SymbolMapper.ToPersistence(element.Symbol));
    }
}