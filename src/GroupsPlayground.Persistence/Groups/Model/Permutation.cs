using System;
using GroupsPlayground.Persistence.Framework;

namespace GroupsPlayground.Persistence.Groups.Model
{
    public class Permutation : Entity
    {
        public Permutation(Guid id, string expression) : base(id)
        {
            Expression = expression;
        }

        public string Expression { get; }
    }
}