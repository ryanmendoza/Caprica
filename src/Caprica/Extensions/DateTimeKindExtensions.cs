using System.Collections.Generic;
using System.Collections.ObjectModel;
using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Collections.Generic.IDictionary`2" />.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        ///     Adds the <paramref name="items" /> to the end of the <paramref name="target" /> <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="items">The items to add.</param>
        /// <returns>
        ///     The merged <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> target, IEnumerable<KeyValuePair<TKey, TValue>> items)
        {
            Guard.IsNotNull(target);

            return target.AddRange(items, false);
        }

        /// <summary>
        ///     Adds the <paramref name="items" /> to the end of the <paramref name="target" /> <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="target">The target.</param>
        /// <param name="items">The elements to add.</param>
        /// <param name="replaceExisting"><c>true</c> to replace existing value in target.</param>
        /// <returns>
        ///     The merged <see cref="T:System.Collections.Generic.IDictionary`2" />.
        /// </returns>
        public static IDictionary<TKey, TValue> AddRange<TKey, TValue>(this IDictionary<TKey, TValue> target, IEnumerable<KeyValuePair<TKey, TValue>> items, bool replaceExisting)
        {
            Guard.IsNotNull(target);

            Guard.IsNotNull(items);

            foreach (var item in items)
            {
                if (replaceExisting)
                {
                    target.Replace(item.Key, item.Value);
                }
                else
                {
                    target.Add(item);
                }
            }

            return target;
        }

        /// <summary>
        ///     Returns a read-only <see cref="T:System.Collections.ObjectModel.IReadOnlyDictionary{TKey,TValue}" /> wrapper for the given <paramref name="target" />.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="target">The target.</param>
        /// <returns>
        ///     A <see cref="T:System.Collections.ObjectModel.IReadOnlyDictionary{TKey,TValue}" /> that acts as a read-only wrapper around the given <paramref name="target" />.
        /// </returns>
        public static IReadOnlyDictionary<TKey, TValue> AsReadOnly<TKey, TValue>(this IDictionary<TKey, TValue> target)
        {
            Guard.IsNotNull(target);

            return new ReadOnlyDictionary<TKey, TValue>(target);
        }

        /// <summary>
        ///     Replaces the value associated with the specified key in the target; otherwise,
        ///     add the specified key/value pair to the target.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <typeparam name="TValue">The type of the value.</typeparam>
        /// <param name="target">The target dictionary.</param>
        /// <param name="key">The key to replace.</param>
        /// <param name="value">The value to replace for the specified key.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static void Replace<TKey, TValue>(this IDictionary<TKey, TValue> target, TKey key, TValue value)
        {
            Guard.IsNotNull(target);

            if (target.ContainsKey(key))
            {
                target[key] = value;
            }
            else
            {
                target.Add(key, value);
            }
        }
    }
}