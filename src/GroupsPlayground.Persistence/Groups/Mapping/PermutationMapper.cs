using System;
using DomainModel = GroupsPlayground.Domain.Groups;
using PersistenceModel = GroupsPlayground.Persistence.Groups.Model;

namespace GroupsPlayground.Persistence.Groups.Mapping
{
    public class PermutationMapper
    {
        public static DomainModel.Permutation ToDomain(PersistenceModel.Permutation permutation) =>
            DomainModel.Permutation.Parse(permutation.Expression);

        public static PersistenceModel.Permutation ToPersistence(DomainModel.Permutation permutation) =>
            new PersistenceModel.Permutation(Guid.NewGuid(), permutation.ToString());
    }
}