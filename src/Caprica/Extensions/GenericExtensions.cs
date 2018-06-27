using System;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of generic types.
    /// </summary>
    public static class GenericExtensions
    {
        /// <summary>
        ///     Determines if the specified target is between the specified lower and upper bounds, inclusive.
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type">type</see> of target to range check.</typeparam>
        /// <param name="target">Target to compare if between <paramref name="lower" /> and <paramref name="upper" />.</param>
        /// <param name="lower">Lower bound in comparison range.</param>
        /// <param name="upper">Upper bound in comparison range.</param>
        /// <returns>
        ///     <c>true</c>, if the target is between the <paramref name="lower" /> and <paramref name="upper" /> bounds; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWithinRange<T>(this T target, T lower, T upper) where T : IComparable<T>
        {
            return target.CompareTo(lower) >= 0 && target.CompareTo(upper) <= 0;
        }
    }
}