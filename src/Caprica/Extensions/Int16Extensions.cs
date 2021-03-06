﻿using System;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Int16" />.
    /// </summary>
    public static class Int16Extensions
    {
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
        public static DateTime FromUnixTimestampToDateTime(this short instance)
        {
            var timeSpan = TimeSpan.FromSeconds(instance);

            return DateTimeExtensions.Epoch.AddNonnegative(timeSpan);
        }
    }
}