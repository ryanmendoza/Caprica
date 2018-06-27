using System;

namespace Caprica.Extensions
{
    /// <summary>
    /// Defines an static class which contains extension methods of number types.
    /// </summary>
    public static class NumberExtensions
    {
        /// <summary>
        /// Compare a number to be between to numbers, inclusive.
        /// </summary>
        /// <typeparam name="T">Number Type</typeparam>
        /// <param name="number">Number to compare if between <paramref name="lower"/> and <paramref name="upper"/>.</param>
        /// <param name="lower">Lower number in comparison range.</param>
        /// <param name="upper">Higher number in comparison range.</param>
        /// <returns>
        /// <c>true</c>, if the number is between the <paramref name="lower"/> and <paramref name="upper"/>; otherwise, <c>false</c>.
        /// </returns>
        public static bool Between<T>(this T number, T lower, T upper) where T : IComparable<T>
        {
            return number.CompareTo(lower) >= 0 && number.CompareTo(upper) <= 0;
        }
    }
}