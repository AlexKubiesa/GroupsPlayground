using System;

namespace GroupsPlayground.Persistence.Model
{
    public class Permutation : Entity
    {
        public Permutation(Guid id) : base(id)
        {
        }

        public string Expression { get; set; }
    }
}