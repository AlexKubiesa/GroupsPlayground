﻿namespace GroupsPlayground.Domain.Framework
{
    public abstract class ValueObject<T>
        where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            if (!(obj is T valueObject))
                return false;

            return EqualsInternal(valueObject);
        }

        protected abstract bool EqualsInternal(T other);

        public override int GetHashCode() => GetHashCodeInternal();

        protected abstract int GetHashCodeInternal();

        public static bool operator ==(ValueObject<T> a, ValueObject<T> b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b) => !(a == b);
    }
}
