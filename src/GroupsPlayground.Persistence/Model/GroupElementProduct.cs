using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Model
{
    public class GroupElementProduct : Entity
    {
        private GroupElementProduct(Guid id) : base(id)
        {
        }

        public GroupElementProduct(Guid id, GroupElement first, GroupElement second, GroupElement product)
            : this(id)
        {
            First = first ?? throw new ArgumentNullException(nameof(first));
            Second = second ?? throw new ArgumentNullException(nameof(second));
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public GroupElement First { get; }
        public GroupElement Second { get; }
        public GroupElement Product { get; }
    }
}