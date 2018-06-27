using System;
using System.Collections.Generic;
using System.Globalization;
using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.String" />.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        ///     Converts the specified value to the specified target type.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="targetType">The <see cref="T:System.Type">type</see> of the target.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static object ConvertToType(this string value, Type targetType)
        {
            Guard.IsNotNull(targetType);

            if (value == null)
            {
                return null;
            }

            if (targetType == typeof (string))
            {
                return value;
            }

            if (targetType.IsEnum)
            {
                return Enum.Parse(targetType, value, true);
            }

            if (targetType.IsNullable())
            {
                targetType = Nullable.GetUnderlyingType(targetType);
            }

            return Convert.ChangeType(value, targetType ?? throw new ArgumentNullException(nameof(targetType)), CultureInfo.InvariantCulture);
        }

        /// <summary>
        ///     Ellipsises the specified target string.
        /// </summary>
        /// <param name="target">The target string to ellipse.</param>
        /// <param name="length">The length to ellipse the string.</param>
        /// <returns>
        ///     The ellipsed string with the default count of three periods ('.').
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static string Ellipsis(this string target, int length)
        {
            Guard.IsNotNull(length);

            Guard.IsNotNegativeOrZero(length);

            return target.Ellipsis(length, 3);
        }

        /// <summary>
        ///     Ellipsises the specified target string.
        /// </summary>
        /// <param name="target">The target string to ellipse.</param>
        /// <param name="length">The length to ellipse the string.</param>
        /// <param name="count">
        ///     The number of periods to place at the end of the string.
        /// </param>
        /// <returns>
        ///     The ellipsed string with the specified number of '.'.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static string Ellipsis(this string target, int length, int count)
        {
            Guard.IsNotNull(length);

            Guard.IsNotNegativeOrZero(length);

            Guard.IsNotNull(count);

            Guard.IsNotNegativeOrZero(count);

            target = target.ToNullSafeString();

            return target.Length <= length ? target : string.Concat(target.Substring(0, length - count), new string('.', count));
        }
        
        /// <summary>
        ///     Formats the specified target string with the specified arguments.
        /// </summary>
        /// <param name="target">The target string to format.</param>
        /// <param name="args">The array of arguments.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the <see cref="T:System.String" /> equivalent of the
        ///     corresponding instances of <see cref="T:System.Object" /> in args.
        /// </returns>
        public static string FormatWith(this string target, params object[] args)
        {
            return target.FormatWith(CultureInfo.CurrentCulture, args);
        }

        /// <summary>
        ///     Formats the specified target string with the specified arguments using the specified format provider.
        /// </summary>
        /// <param name="target">The target string to format.</param>
        /// <param name="provider">The provider.</param>
        /// <param name="args">The array of arguments.</param>
        /// <returns>
        ///     A copy of format in which the format items have been replaced by the <see cref="T:System.String" /> equivalent of the
        ///     corresponding instances of <see cref="T:System.Object" /> in args.
        /// </returns>
        public static string FormatWith(this string target, IFormatProvider provider, params object[] args)
        {
            Guard.IsNotEmpty(target);

            Guard.IsNotNull(provider);

            Guard.IsNotEmpty(args);

            return string.Format(provider, target, args);
        }

        /// <summary>
        ///     Indicates whether the specified target string is a json string.
        /// </summary>
        /// <param name="target">The target sting to test.</param>
        /// <returns>
        ///     <c>true</c> if the target string is is a json string; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsJson(this string target)
        {
            target = target.Trim();

            return target.StartsWith("{") && target.EndsWith("}") || target.StartsWith("[") && target.EndsWith("]");
        }
        
        /// <summary>
        ///     Indicates whether the specified target string is <c>null</c> or an empty string.
        /// </summary>
        /// <param name="target">The target sting to test.</param>
        /// <returns>
        ///     <c>true</c> if the target string is <c>null</c> or an empty string (""); otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this string target)
        {
            return string.IsNullOrEmpty(target);
        }

        /// <summary>
        ///     Indicates whether a specified string is <c>null</c>, <see cref="F:System.String.Empty" />, or consists only of white-space characters.
        /// </summary>
        /// <param name="target">The target sting to test.</param>
        /// <returns>
        ///     <c>true</c> if the target string is <c>null</c> or <see cref="F:System.String.Empty" />, or if the target string consists exclusively of white-space characters.
        /// </returns>
        public static bool IsNullOrWhiteSpace(this string target)
        {
            return string.IsNullOrWhiteSpace(target);
        }

        /// <summary>
        ///     Normalizes the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="allowEmpty">if set to <c>true</c> [allow empty].</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static string Normalize(this string target, bool allowEmpty)
        {
            return allowEmpty ? target.ToNullSafeString() : target.TrimIfNotNull();
        }

        /// <summary>
        ///     Replaces the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="replacements">The replacements.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static string Replace(this string target, IEnumerable<KeyValuePair<string, string>> replacements)
        {
            return target.Replace("{0}", "{0}", replacements);
        }

        /// <summary>
        ///     Replaces the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="oldValueFormat">The old value format.</param>
        /// <param name="newValueFormat">The new value format.</param>
        /// <param name="replacements">The replacements.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static string Replace(this string target, string oldValueFormat, string newValueFormat, IEnumerable<KeyValuePair<string, string>> replacements)
        {
            Guard.IsNotNull(target);

            Guard.IsNotEmpty(oldValueFormat);

            Guard.IsNotEmpty(newValueFormat);

            Guard.IsNotNull(replacements);

            replacements.ForEach(replacement => target = target.Replace(oldValueFormat.FormatWith(replacement.Key), newValueFormat.FormatWith(replacement.Value)));

            return target;
        }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target">The target.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static T ToEnum<T>(this string target, T defaultValue) where T : IComparable, IFormattable
        {
            var convertedValue = defaultValue;

            if (target.IsNullOrEmpty())
            {
                return convertedValue;
            }

            try
            {
                convertedValue = (T) Enum.Parse(typeof (T), target.Trim(), true);
            }
            catch (ArgumentException)
            {
            }

            return convertedValue;
        }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static Guid ToGuid(this string target)
        {
            var result = Guid.Empty;

            if (!target.IsNullOrEmpty() && target.Trim().Length.Equals(22))
            {
                var encoded = string.Concat(target.Trim().Replace("-", "+").Replace("_", "/"), "==");

                try
                {
                    var base64 = Convert.FromBase64String(encoded);

                    result = new Guid(base64);
                }
                catch (FormatException)
                {
                }
            }

            return result;
        }

        /// <summary>
        ///     Safe guards the target string from being <c>null</c> by converting it to an empty string if in fact it is null.
        /// </summary>
        /// <param name="target">The target string to safe guard.</param>
        /// <returns>
        ///     The trimmed target string; otherwise, an empty string ("").
        /// </returns>
        public static string ToNullSafeString(this string target)
        {
            return target.ToNullSafeString(string.Empty);
        }

        /// <summary>
        ///     Safe guards the target string from being <c>null</c> by converting it to an empty string if in fact it is null.
        /// </summary>
        /// <param name="target">The target string to safe guard.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The trimmed target string; otherwise, an empty string ("").
        /// </returns>
        public static string ToNullSafeString(this string target, string defaultValue)
        {
            var targetAsAString = target ?? string.Empty;

            return (targetAsAString.IsNullOrEmpty() ? defaultValue : targetAsAString).Trim();
        }

        /// <summary>
        ///     Trims the specified target string if not null.
        /// </summary>
        /// <param name="target">The target string to trim.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static string TrimIfNotNull(this string target)
        {
            return target?.Trim();
        }

        /// <summary>
        ///     Tries to parse the value checking for a <c>null</c> or an empty string. If the value is an empty string, the specified
        ///     default value will be returned; otherwise an emtpy <see cref="T:System.String" /> if no default value is specified.
        /// </summary>
        /// <param name="target">The target string to test.</param>
        /// <param name="defaultValue">The default return value.</param>
        /// <returns>
        ///     The specified value if not <c>null</c> or empty; otherwise the specified default value.
        /// </returns>
        public static string TryParse(this string target, string defaultValue)
        {
            return target.IsNullOrEmpty() ? defaultValue.ToNullSafeString() : target;
        }
    }
}