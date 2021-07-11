using System;

namespace GroupsPlayground.Persistence.Model
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