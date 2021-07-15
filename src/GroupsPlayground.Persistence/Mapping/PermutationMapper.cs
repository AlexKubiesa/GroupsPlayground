
using System;
using GroupsPlayground.Domain.Groups;

namespace GroupsPlayground.Persistence.Mapping
{
    public class PermutationMapper
    {
        public static Permutation ToDomain(Model.Permutation permutation) =>
            Permutation.Parse(permutation.Expression);

        public static Model.Permutation ToPersistence(Permutation permutation) =>
            new Model.Permutation(Guid.NewGuid(), permutation.ToString());
    }
}