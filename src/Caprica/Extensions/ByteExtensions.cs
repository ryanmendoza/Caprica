using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Byte" />.
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string representation that is encoded with base-64 digits.
        /// </summary>
        /// <param name="target">An array of 8-bit unsigned integers.</param>
        /// <returns>
        ///     The string representation, in base 64, of the contents of <paramref name="target" />.
        /// </returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="target" /> is null.</exception>
        /// <filterpriority>1</filterpriority>
        public static string ToBase64String(this byte[] target)
        {
            return target == null ? null : System.Convert.ToBase64String(target);
        }

        /// <summary>
        ///     Converts an array of 8-bit unsigned integers to its equivalent string hexadecimal representation.
        /// </summary>
        /// <param name="target">An array of 8-bit unsigned integers.</param>
        /// <returns>
        ///     The string representation, in hexadecimal, of the contents of <paramref name="target" />.
        /// </returns>
        public static string ToHexString(this byte[] target)
        {
            Guard.IsNotNull(target);

            var c = new char[target.Length * 2];

            for (var i = 0; i < target.Length; i++)
            {
                var b = target[i] >> 4;

                c[i * 2] = (char) (55 + b + (((b - 10) >> 31) & -7));

                b = target[i] & 0xF;

                c[i * 2 + 1] = (char) (55 + b + (((b - 10) >> 31) & -7));
            }

            return new string(c);
        }
    }
}