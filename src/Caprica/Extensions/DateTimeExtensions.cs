using System;
using System.Globalization;
using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.DateTime" />.
    /// </summary>
    public static class DateTimeExtensions
    {
        /// <summary>
        /// The epoch
        /// </summary>
        public static readonly DateTime Epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        ///     Adds the specified <see cref="T:System.TimeSpan" /> to the <see cref="T:System.DateTime" /> instance..
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="timeSpan">The time span.</param>
        /// <returns>
        ///     The DateTime instance with the added TimeSpan.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">TimeSpan must be greater than or equal to TimeSpan.Zero.</exception>
        public static DateTime AddNonnegative(this DateTime instance, TimeSpan timeSpan)
        {
            Guard.IsNotNegative(timeSpan);

            return instance.Add(timeSpan);
        }

        /// <summary>
        ///     Determines whether the specified target is valid.
        /// </summary>
        /// <param name="instance"> The <see cref="T:System.DateTime" /> instance to check if valid. </param>
        /// <returns>
        ///     <c>true</c> if the specified target is valid; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsValid(this DateTime instance)
        {
            instance = DateTime.SpecifyKind(instance, DateTimeKind.Utc);

            return instance >= DateTime.MinValue.ToUniversalTime() && instance <= DateTime.MaxValue.ToUniversalTime();
        }

        /// <summary>
        ///     Finds the next <see cref="T:System.DateTime" /> whose <see cref="T:System.DayOfWeek" /> equals the specified <see cref="T:System.DayOfWeek" />.
        /// </summary>
        /// <param name="instance">The instance to search from.</param>
        /// <param name="dayOfWeek">The desired day of the week whose date will be returned.</param>
        /// <returns>
        ///     The returned date occurs on the given date's week. If the given day occurs before
        ///     given date, the date for the following week's desired day is returned.
        /// </returns>
        public static DateTime NextDateTimeForDayOfWeek(this DateTime instance, DayOfWeek dayOfWeek)
        {
            // g( c, d ) = [7 - (c - d)] = 7 - c + d
            //                  when 0 <= c < 7 and 0 <= d < 7

            // f( c, d ) = g( c, d ) mod 7
            //                  when g( c, d ) > 7
            // f( c, d ) = g( c, d )
            //                  when g( c, d ) <= 7

            var current = (int) instance.DayOfWeek;

            var desired = (int) dayOfWeek;

            var n = 7 - current + desired;

            return instance.AddDays(n > 7 ? n%7 : n);
        }

        /// <summary>
        ///     Finds the previous <see cref="T:System.DateTime" /> whose <see cref="T:System.DayOfWeek" /> equals the specified <see cref="T:System.DayOfWeek" />.
        /// </summary>
        /// <param name="instance">The instance to search from.</param>
        /// <param name="dayOfWeek">The desired day of the week whose date will be returned.</param>
        /// <returns>
        ///     The returned date occurs on the given date's week. If the given day occurs before
        ///     given date, the date for the following week's desired day is returned.
        /// </returns>
        public static DateTime PreviousDateTimeForDayOfWeek(this DateTime instance, DayOfWeek dayOfWeek)
        {
            // g( c, d ) = -(7 - d + c) = -7 + d - c
            //                  when 0 <= c < 7 and 0 <= d < 7

            // f( c, d ) = g( c, d ) mod 7
            //                  when g( c, d ) < -7
            // f( c, d ) = g( c, d )
            //                  when g( c, d ) >= -7

            var current = (int) instance.DayOfWeek;

            var desired = (int) dayOfWeek;

            var n = -7 + desired - current;

            return instance.AddDays(n < -7 ? n%7 : n);
        }

        /// <summary>
        ///     Converts the specified <see cref="T:System.DateTime" /> instance to Unix timestamp.
        /// </summary>
        /// <param name="instance"> The instance to convert. </param>
        /// <remarks>
        ///     Converts the specified <see cref="T:System.DateTime" /> to Utc before conversion.
        /// </remarks>
        /// <returns>
        ///     The Unix timestamp.
        /// </returns>
        public static long ToUnixTimestamp(this DateTime instance)
        {
            if (instance < Epoch)
            {
                throw new ArgumentOutOfRangeException(nameof(instance), $"DateTime must be greater than or equal to '{Epoch.ToString(CultureInfo.InvariantCulture)}'.");
            }

            instance = DateTime.SpecifyKind(instance, DateTimeKind.Utc);

            return (long) instance.Subtract(Epoch).TotalSeconds;
        }
    }
}