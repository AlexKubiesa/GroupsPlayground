﻿using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Persistence.Model
{
    public class GroupElement : Entity
    {
        private Symbol symbol;

        public GroupElement(Guid id, Symbol symbol) : base(id)
        {
            Symbol = symbol;
        }

        public Symbol Symbol
        {
            get => symbol;
            set => symbol = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}