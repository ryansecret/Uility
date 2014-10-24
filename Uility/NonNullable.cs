using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uility
{
    public class NonNullable<T> : IEquatable<NonNullable<T>> where T : class 
    {
        private readonly T value;

        /// <summary>
        /// Creates a non-nullable value encapsulating the specified reference.
        /// </summary>
        public NonNullable(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
            this.value = value;
        }

        /// <summary>
        /// Retrieves the encapsulated value, or throws a NullReferenceException if
        /// this instance was created with the parameterless constructor or by default.
        /// </summary>
        public T Value
        {
            get
            {
                if (value == null)
                {
                    throw new NullReferenceException();
                }
                return value;
            }
        }

        /// <summary>
        /// Implicit conversion from the specified reference.
        /// </summary>
        public static implicit operator NonNullable<T>(T value)
        {
            return new NonNullable<T>(value);
        }

        /// <summary>
        /// Implicit conversion to the type parameter from the encapsulated value.
        /// </summary>
        public static implicit operator T(NonNullable<T> wrapper)
        {
            return wrapper.Value;
        }

        /// <summary>
        /// Equality operator, which performs an identity comparison on the encapuslated
        /// references. No exception is thrown even if the references are null.
        /// </summary>
        public static bool operator ==(NonNullable<T> first, NonNullable<T> second)
        {
            return first.value == second.value;
        }

        /// <summary>
        /// Inequality operator, which performs an identity comparison on the encapuslated
        /// references. No exception is thrown even if the references are null.
        /// </summary>
        public static bool operator !=(NonNullable<T> first, NonNullable<T> second)
        {
            return first.value != second.value;
        }
         

        /// <summary>
        /// Equality is deferred to encapsulated references, but there is no equality
        /// between a NonNullable[T] and a T. This method never throws an exception,
        /// even if a null reference is encapsulated.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (!(obj is NonNullable<T>))
            {
                return false;
            }
            return Equals((NonNullable<T>)obj);
        }

        /// <summary>
        /// Type-safe (and non-boxing) equality check.
        /// </summary>
        public bool Equals(NonNullable<T> other)
        {
            return object.Equals(this.value, other.value);
        }

        /// <summary>
        /// Type-safe (and non-boxing) static equality check.
        /// </summary>
        public static bool Equals(NonNullable<T> first, NonNullable<T> second)
        {

            return object.Equals(first.value, second.value);
        }

        /// <summary>
        /// Defers to the GetHashCode implementation of the encapsulated reference, or 0 if
        /// the reference is null.
        /// </summary>
        public override int GetHashCode()
        {
            return value == null ? 0 : value.GetHashCode();
        }

        /// <summary>
        /// Defers to the ToString implementation of the encapsulated reference, or an
        /// empty string if the reference is null.
        /// </summary>
        public override string ToString()
        {
            return value == null ? "" : value.ToString();
        }
    }
}
