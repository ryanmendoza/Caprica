using System.Collections.Generic;
using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Collections.Generic.ICollection`1" />.
    /// </summary>
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Adds the specified items to the specified collection.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type">type</see> of items in the collection.</typeparam>
        /// <param name="collection">The collection to recieve the items.</param>
        /// <param name="items">The items to add.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, params T[] items)
        {
            Guard.IsNotNull(collection);

            Guard.IsNotNull(items);

            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }

        /// <summary>
        ///     Adds the specified items to the specified collection.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type">type</see> of items in the collection.</typeparam>
        /// <param name="collection">The collection to recieve the items.</param>
        /// <param name="items">The items to add.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        public static ICollection<T> AddRange<T>(this ICollection<T> collection, IEnumerable<T> items)
        {
            Guard.IsNotNull(collection);

            Guard.IsNotNull(items);

            foreach (var item in items)
            {
                collection.Add(item);
            }

            return collection;
        }
    }
}