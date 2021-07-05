﻿using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain
{
    public class GroupElementProduct : Entity
    {
        private GroupElementProduct(Guid id) : base(id)
        {
        }

        public GroupElementProduct(Guid id, IGroupElement first, IGroupElement second, IGroupElement product)
            : this(id)
        {
            First = first ?? throw new ArgumentNullException(nameof(first));
            Second = second ?? throw new ArgumentNullException(nameof(second));
            Product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public IGroupElement First { get; }
        public IGroupElement Second { get; }
        public IGroupElement Product { get; }
    }
}