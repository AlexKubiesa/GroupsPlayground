﻿using System;
using GroupsPlayground.Domain.Framework;

namespace GroupsPlayground.Domain.Quests
{
    public class Quest : Entity
    {
        public Quest(Guid id) : base(id)
        {
        }

        public bool Complete { get; set; }
    }
}