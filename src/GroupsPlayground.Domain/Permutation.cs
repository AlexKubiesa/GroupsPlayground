using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public sealed class Permutation : ValueObject<Permutation>
    {
        public static Permutation Parse(string expression)
        {
            throw new NotImplementedException();
        }

        protected override bool EqualsInternal(Permutation other)
        {
            throw new System.NotImplementedException();
        }

        protected override int GetHashCodeInternal()
        {
            throw new System.NotImplementedException();
        }
    }
}