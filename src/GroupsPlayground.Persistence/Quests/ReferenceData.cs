using System;
using System.Collections.Generic;
using GroupsPlayground.Persistence.Quests.Model;

namespace GroupsPlayground.Persistence.Quests
{
    internal static class ReferenceData
    {
        public static IEnumerable<Quest> Quests = new[]
        {
            new Quest(Guid.Parse("C5DA5525-2A22-4F1D-A8C6-1458A9D632F9")) { Description = "Create a group" },
            new Quest(Guid.Parse("B4B6C007-0EB8-4C93-9B89-53EFDD5F6BF0")) { Description = "Compute the size of a group" }
    };
    }
}