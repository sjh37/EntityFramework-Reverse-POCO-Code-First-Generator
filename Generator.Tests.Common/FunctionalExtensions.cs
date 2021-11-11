using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Generator.Tests.Common
{
    [DebuggerStepThrough]
    [ExcludeFromCodeCoverage]
    public static class FunctionalExtensions
    {
        public static TOut Map<TIn, TOut>(this TIn @this, Func<TIn, TOut> func)
            where TIn : class
        {
            return @this == null ? default : func(@this);
        }

        public static T Then<T>(this T @this, Action<T> then)
            where T : class
        {
            if (@this != null) then(@this);
            return @this;
        }

        public static ICollection<T> AsCollection<T>(this T @this) where T : class
            => @this == null ? Array.Empty<T>() : new[] { @this };

        public static void Each<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this == null) return;
            foreach (var element in @this)
            {
                action(element);
            }
        }
    }
}
