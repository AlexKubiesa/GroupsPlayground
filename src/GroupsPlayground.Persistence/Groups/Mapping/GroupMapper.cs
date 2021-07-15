using System;
using System.Collections.Generic;
using System.Linq;
using GroupsPlayground.Domain.Framework;
using DomainModel = GroupsPlayground.Domain.Groups;
using PersistenceModel = GroupsPlayground.Persistence.Groups.Model;

namespace GroupsPlayground.Persistence.Groups.Mapping
{
    public static class GroupMapper
    {
        public static DomainModel.Group ToDomain(PersistenceModel.Group group) =>
            @group switch
            {
                PersistenceModel.CayleyTableGroup cayleyTableGroup => ToDomain(cayleyTableGroup),
                PersistenceModel.PermutationGroup permutationGroup => ToDomain(permutationGroup),
                _ => throw new ArgumentOutOfRangeException(nameof(@group))
            };

        public static DomainModel.CayleyTableGroup ToDomain(PersistenceModel.CayleyTableGroup group)
        {
            var symbols =
                new DomainModel.GroupElementSymbols(group.Elements.Select(x => SymbolMapper.ToDomain(x.Symbol)).ToValueList());

            var cayleyTable = new DomainModel.CayleyTable(Guid.NewGuid(), symbols);
            foreach (var product in group.Products)
            {
                int row = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.First.Symbol));
                int column = cayleyTable.Symbols.IndexOf(SymbolMapper.ToDomain(product.Second.Symbol));
                cayleyTable.Products[row][column] = SymbolMapper.ToDomain(product.Product.Symbol);
            }

            return new DomainModel.CayleyTableGroup(group.Id, group.Name, cayleyTable);
        }

        public static DomainModel.PermutationGroup ToDomain(PersistenceModel.PermutationGroup group) =>
            new DomainModel.PermutationGroup(@group.Id, @group.Name, group.Generators.Select(PermutationMapper.ToDomain).ToValueList(), @group.Size);

        public static PersistenceModel.Group ToPersistence(DomainModel.Group group) =>
            @group switch
            {
                DomainModel.CayleyTableGroup cayleyTableGroup => ToPersistence(cayleyTableGroup),
                DomainModel.PermutationGroup permutationGroup => ToPersistence(permutationGroup),
                _ => throw new ArgumentOutOfRangeException(nameof(@group))
            };

        public static PersistenceModel.CayleyTableGroup ToPersistence(DomainModel.CayleyTableGroup group)
        {
            var elements = group.Elements.ToDictionary(x => x, ToPersistence);
            var products = new List<PersistenceModel.GroupElementProduct>(group.Elements.Count * group.Elements.Count);
            products.AddRange(
                from first in elements.Keys
                from second in elements.Keys
                select new PersistenceModel.GroupElementProduct(Guid.NewGuid(), elements[first], elements[second],
                    elements[@group.Multiply(first, second)]));

            return new PersistenceModel.CayleyTableGroup(group.Id, group.Name)
            {
                Elements = elements.Values.ToArray(),
                Products = products
            };
        }

        public static PersistenceModel.PermutationGroup ToPersistence(DomainModel.PermutationGroup group) =>
            new PersistenceModel.PermutationGroup(@group.Id, group.Name)
            {
                Size = group.Size,
                Generators = group.Generators.Select(PermutationMapper.ToPersistence).ToArray()
            };

        private static PersistenceModel.GroupElement ToPersistence(DomainModel.IGroupElement element) =>
            new PersistenceModel.GroupElement(element.Id, SymbolMapper.ToPersistence(element.Symbol));
    }
}