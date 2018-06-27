using System;
using System.Net;
using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Net.IPAddress" />.
    /// </summary>
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", MessageId = "Extentions")]
    public static class IpAddressExtentions
    {
        /// <summary>
        ///     Determines whether the specified IP address is within the lower and upper bounds.
        /// </summary>
        /// <param name="address">The address to check.</param>
        /// <param name="lower">The lower bound.</param>
        /// <param name="upper">The upper bound.</param>
        /// <returns>
        ///     <c>true</c> if the specified IP address is within range; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsWithinRange(this IPAddress address, string lower, string upper)
        {
            Guard.IsNotNull(address);

            return address.IsWithinRange(IPAddress.Parse(lower), IPAddress.Parse(upper));
        }

        /// <summary>
        ///     Determines whether the specified IP address is within the lower and upper bounds.
        /// </summary>
        /// <param name="address">The address to check.</param>
        /// <param name="lower">The lower bound.</param>
        /// <param name="upper">The upper bound.</param>
        /// <returns>
        ///     <c>true</c> if the specified IP address is within range; otherwise, <c>false</c>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "1")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "2")]
        public static bool IsWithinRange(this IPAddress address, IPAddress lower, IPAddress upper)
        {
            Guard.IsNotNull(address);

            Guard.IsNotNull(lower);

            Guard.IsNotNull(upper);

            var returnVal = false;

            if (address.AddressFamily.Equals(lower.AddressFamily) && address.AddressFamily.Equals(upper.AddressFamily))
            {
                returnVal = address.CompareTo(lower) >= 0 && address.CompareTo(upper) <= 0;
            }

            return returnVal;
        }

        private static int CompareTo(this IPAddress address, IPAddress value)
        {
            Guard.IsNotNull(address);

            Guard.IsNotNull(value);

            var returnVal = 0;

            if (address.AddressFamily.Equals(value.AddressFamily))
            {
                var b1 = address.GetAddressBytes();

                var b2 = value.GetAddressBytes();

                for (var i = 0; i < b1.Length; i++)
                {
                    if (b1[i] < b2[i])
                    {
                        returnVal = -1;

                        break;
                    }

                    if (b1[i] <= b2[i])
                    {
                        continue;
                    }

                    returnVal = 1;

                    break;
                }
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Cannot compare two addresses not in the same Address Family.");
            }

            return returnVal;
        }
    }
}