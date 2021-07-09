using System;
using System.Collections.Generic;

namespace GroupsPlayground.Blazor.Framework
{
    public sealed class Cursor<T>
    {
        private readonly IReadOnlyList<T> list;

        public Cursor(IReadOnlyList<T> list)
        {
            this.list = list ?? throw new ArgumentNullException(nameof(list));
        }

        public int Index { get; set; }

        public bool IsInRange() => (Index >= 0) && (Index < list.Count);

        public T GetItem() => IsInRange() ? list[Index] : default;
    }
}