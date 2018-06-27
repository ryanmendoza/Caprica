using System;
using System.Collections.Generic;
using System.Linq;
using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Collections.Generic.IEnumerable`1" />.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        ///     Executes the <paramref name="action" /> for each element of <paramref name="source" />.
        /// </summary>
        /// <typeparam name="TSource">Type of items in <paramref name="source" />.</typeparam>
        /// <param name="source">Source of items to act on.</param>
        /// <param name="action">Action to invoke for each item.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> action)
        {
            Guard.IsNotNull(action);

            if (source == null)
            {
                return;
            }

            foreach (var item in source)
            {
                action(item);
            }
        }

        /// <summary>
        ///     Determines whether the specified enumerable is <c>null</c> or empty.
        /// </summary>
        /// <typeparam name="T">The type of items in the enumerable.</typeparam>
        /// <param name="source">The enumerable to check.</param>
        /// <returns>
        ///     <c>true</c> if the specified enumerable is <c>null</c> or empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
    }
}