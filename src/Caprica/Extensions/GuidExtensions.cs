using System;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Guid" />.
    /// </summary>
    public static class GuidExtensions
    {
        /// <summary>
        ///     Determines whether the specified <see cref="T:System.Guid" /> is empty.
        /// </summary>
        /// <param name="target">The target <see cref="T:System.Guid" /> to check.</param>
        /// <returns>
        ///     <c>true</c> if the specified <see cref="T:System.Guid" /> is empty; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsEmpty(this Guid target)
        {
            return Guid.Empty.Equals(target);
        }
    }
}