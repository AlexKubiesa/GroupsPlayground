using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class GroupElementProduct : Entity
    {
        public GroupElementProduct(Guid id) : base(id)
        {
        }

        public GroupElement First { get; set; }
        public GroupElement Second { get; set; }
        public GroupElement Product { get; set; }
    }
}