using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;
using GroupsPlayground.Domain.Groups;

namespace GroupsPlayground.Persistence.Mapping
{
    public static class GroupMapper
    {
        public static Group ToDomain(Model.Group group) =>
            @group switch
            {
                Model.CayleyTableGroup cayleyTableGroup => ToDomain(cayleyTableGroup),
                Model.PermutationGroup permutationGroup => ToDomain(permutationGroup),
                _ => throw new ArgumentOutOfRangeException(nameof(@group))
            };

        public static CayleyTableGroup ToDomain(Model.CayleyTableGroup group)
        {
            var symbols =
                new GroupElementSymbols(group.Elements.Select(x => SymbolMapper.ToDomain(x.Symbol)).ToValueList());

            var cayleyTable = new CayleyTable(Guid.NewGuid(), symbols);
            foreach (var product in group.Products)
            {
                int row = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.First.Symbol));
                int column = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.Second.Symbol));
                cayleyTable.Products[row][column] = SymbolMapper.ToDomain(product.Product.Symbol);
            }

            return new CayleyTableGroup(group.Id, group.Name, cayleyTable);
        }

        public static PermutationGroup ToDomain(Model.PermutationGroup group) =>
            new PermutationGroup(@group.Id, @group.Name, group.Generators.Select(PermutationMapper.ToDomain).ToValueList(), @group.Size);

        public static Model.Group ToPersistence(Group group) =>
            @group switch
            {
                CayleyTableGroup cayleyTableGroup => ToPersistence(cayleyTableGroup),
                PermutationGroup permutationGroup => ToPersistence(permutationGroup),
                _ => throw new ArgumentOutOfRangeException(nameof(@group))
            };

        public static Model.CayleyTableGroup ToPersistence(CayleyTableGroup group)
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

        public static Model.PermutationGroup ToPersistence(PermutationGroup group) =>
            new Model.PermutationGroup(@group.Id, group.Name)
            {
                Size = group.Size,
                Generators = group.Generators.Select(PermutationMapper.ToPersistence).ToArray()
            };

        private static Model.GroupElement ToPersistence(IGroupElement element) =>
            new Model.GroupElement(element.Id, SymbolMapper.ToPersistence(element.Symbol));
    }
}