using System;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Double" />.
    /// </summary>
    public static class DoubleExtensions
    {
        /// <summary>
        /// </summary>
        private static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     Converts the specified Unix timestamp to a <see cref="T:System.DateTime" /> representation.
        /// </summary>
        /// <param name="instance">The instance to convert.</param>
        /// <remarks>
        ///     The returned <see cref="T:System.DateTime" /> will be in Utc.
        /// </remarks>
        /// <returns>
        ///     The <see cref="T:System.DateTime" /> representation of the specified Unix timestamp.
        /// </returns>
        public static DateTime FromUnixTimestampToDateTime(this double instance)
        {
            return Epoch.AddSeconds(instance);
        }
    }
}