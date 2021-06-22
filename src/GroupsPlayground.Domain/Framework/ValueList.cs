using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace GroupsPlayground.Domain.Framework
{
    public sealed class ValueList<T> : ValueObject<ValueList<T>>, IReadOnlyList<T>
    {
        private readonly IReadOnlyList<T> inner;
        private readonly int hashCode;

        public ValueList(IReadOnlyList<T> inner)
        {
            this.inner = inner ?? throw new ArgumentNullException(nameof(inner));
            hashCode = ComputeHashCode(inner);
        }

        public ValueList(params T[] inner) : this((IReadOnlyList<T>)inner)
        {
        }

        private static int ComputeHashCode(IEnumerable<T> source) =>
            source.Select(EqualityComparer<T>.Default.GetHashCode).Aggregate(0, System.HashCode.Combine);

        protected override bool EqualsInternal(ValueList<T> other) =>
            (Count == other.Count) && this.Zip(other, EqualityComparer<T>.Default.Equals).All(x => x);

        protected override int GetHashCodeInternal() => hashCode;

        public IEnumerator<T> GetEnumerator() => inner.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public int Count => inner.Count;

        public T this[int index] => inner[index];
    }
}