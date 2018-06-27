using System;
using System.Collections.Generic;
using System.Linq;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of list types.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        ///     Initializes the specified list.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="value">The value.</param>
        /// <param name="count">The count.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static List<T> Initialize<T>(this List<T> list, T value, int count)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            list.AddRange(Enumerable.Repeat(value, count));

            return list;
        }
    }
}