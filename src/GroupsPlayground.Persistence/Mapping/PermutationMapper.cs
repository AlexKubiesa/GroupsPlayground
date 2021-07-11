
using System;

namespace GroupsPlayground.Persistence.Mapping
{
    public class PermutationMapper
    {
        public static Domain.Permutation ToDomain(Model.Permutation permutation) =>
            Domain.Permutation.Parse(permutation.Expression);

        public static Model.Permutation ToPersistence(Domain.Permutation permutation) =>
            new Model.Permutation(Guid.NewGuid(), permutation.ToString());
    }
}