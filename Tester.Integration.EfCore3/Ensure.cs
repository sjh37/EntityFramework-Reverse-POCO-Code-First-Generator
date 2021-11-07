using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Tester.Integration.EfCore3
{
    [DebuggerStepThrough]
    [ExcludeFromCodeCoverage]
    internal static class Ensure
    {

        internal static void EnsureIsNotNull(this object subject, string name = null, string message = "")
        {
            IsNotNull(subject, name, message);
        }

        internal static void IsNotNull(object subject, string name = null, string message = "")
        {
            if (subject == null)
            {
                throw new ArgumentNullException(name ?? nameof(subject), message ?? "Object cannot be null");
            }
        }

        internal static void IsNotNullOrEmpty(string subject, string name = null, string message = "")
        {
            if (string.IsNullOrWhiteSpace(subject))
            {
                throw new ArgumentNullException(name ?? nameof(subject), message ?? "Object cannot be null or empty");
            }
        }
    }

    [DebuggerStepThrough]
    [ExcludeFromCodeCoverage]
    internal static class FunctionalExtensions
    {

        internal static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> func)
            where TIn : class
        {
            return @this == null ? default(TOut) : func(@this);
        }

        internal static T Then<T>(this T @this, Action<T> then)
            where T : class
        {
            if (@this != null) then(@this);
            return @this;
        }

        internal static ICollection<T> AsCollection<T>(this T @this) where T : class
            => @this == null ? new T[0] : new[] { @this };

        internal static void Each<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this == null) return;
            foreach (var element in @this)
            {
                action(element);
            }
        }
    }
}