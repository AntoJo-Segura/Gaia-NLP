// <copyright file="ValueObject.cs" company="Gaia">
// Gaia Natural Language Processing
// </copyright>

using System.Collections.Generic;
using System.Linq;

namespace Gbm.Cash.Spei.Domain.SeedWork
{
    /// <summary>
    /// ValueObject.
    /// </summary>
    public abstract class ValueObject
    {
        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObject other = (ValueObject)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();
            while (thisValues.MoveNext() && otherValues.MoveNext())
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }
            }

            return !thisValues.MoveNext() && !otherValues.MoveNext();
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            return GetAtomicValues()
             .Select(x => x != null ? x.GetHashCode() : 0)
             .Aggregate((x, y) => x ^ y);
        }

        /// <summary>
        /// Gets the copy.
        /// </summary>
        /// <returns>Value object.</returns>
        public ValueObject GetCopy()
        {
            return MemberwiseClone() as ValueObject;
        }

        /// <summary>
        /// Equals the operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>Result.</returns>
        protected static bool EqualOperator(ValueObject left, ValueObject right)
        {
            if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
            {
                return false;
            }

            return ReferenceEquals(left, null) || left.Equals(right);
        }

        /// <summary>
        /// Nots the equal operator.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>Result.</returns>
        protected static bool NotEqualOperator(ValueObject left, ValueObject right)
        {
            return !EqualOperator(left, right);
        }

        /// <summary>
        /// Gets the atomic values.
        /// </summary>
        /// <returns>Atomic values.</returns>
        protected abstract IEnumerable<object> GetAtomicValues();
    }
}
