using System;
using System.Collections.Generic;
using Caprica.Extensions;

namespace Caprica.Helpers
{
    /// <summary>
    ///     A static helper class that includes various parameter checking routines.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        ///     Determines whether the specified argument is empty. If empty, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <typeparam name="T">The specified <paramref name="argument" />'s item <see cref="T:System.Type">type</see>.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentNullException">Collection argument is null.</exception>
        /// <exception cref="T:System.ArgumentException">Collection argument cannot be empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotEmpty<T>(IEnumerable<T> argument)
        {
            IsNotEmpty(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is empty. If empty, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <typeparam name="T">The specified <paramref name="argument" />'s item <see cref="T:System.Type">type</see>.</typeparam>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the argument.</param>
        /// <exception cref="T:System.ArgumentNullException">Collection argument is null.</exception>
        /// <exception cref="T:System.ArgumentException">Collection argument cannot be empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotEmpty<T>(IEnumerable<T> argument, string paramName)
        {
            if (argument.IsNullOrEmpty())
            {
                throw new ArgumentException($"'{paramName}' cannot be empty.", paramName);
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is empty. If empty, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentException">Guid argument cannot be empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotEmpty(Guid argument)
        {
            IsNotEmpty(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is empty. If empty, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the argument.</param>
        /// <exception cref="T:System.ArgumentException">Guid argument cannot be empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotEmpty(Guid argument, string paramName)
        {
            if (argument.IsEmpty())
            {
                throw new ArgumentException($"'{paramName}' cannot be empty.", paramName);
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is empty. If empty, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentException">String argument cannot be null or empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotEmpty(string argument)
        {
            IsNotEmpty(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is empty. If empty, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentException">String argument cannot be null or empty.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotEmpty(string argument, string paramName)
        {
            if (argument.ToNullSafeString().IsNullOrEmpty())
            {
                throw new ArgumentException($"'{paramName}' cannot be null or empty.", paramName);
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is in the future. If in the future, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument, converted to universal time.</param>
        /// <exception cref="T:System.ArgumentNullException">DateTime argument is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">DateTime argument is in the future.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotInFuture(DateTime argument)
        {
            IsNotInFuture(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is in the future. If in the future, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument, converted to universal time.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentNullException">DateTime argument is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">DateTime argument is in the future.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotInFuture(DateTime argument, string paramName)
        {
            IsNotNull(argument, paramName);

            if (argument.ToUniversalTime() > DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException($"'{paramName}' cannot be in the future.", paramName);
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is in the past. If in the past, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument, converted to universal time.</param>
        /// <exception cref="T:System.ArgumentNullException">DateTime argument is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">DateTime argument is in the past.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotInPast(DateTime argument)
        {
            IsNotInPast(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is in the past. If in the past, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument, converted to universal time.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentNullException">DateTime argument is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException">DateTime argument is in the past.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotInPast(DateTime argument, string paramName)
        {
            IsNotNull(argument, paramName);

            if (argument.ToUniversalTime() < DateTime.UtcNow)
            {
                throw new ArgumentOutOfRangeException($"'{paramName}' cannot be in the past.", paramName);
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is not a valid date. If not a valid date, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument, converted to universal time.</param>
        /// <exception cref="T:System.ArgumentNullException">DateTime argument is null.</exception>
        /// <exception cref="T:System.ArgumentException">DateTime argument is not a valid date.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotInvalidDate(DateTime argument)
        {
            IsNotInvalidDate(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is not a valid date. If not a valid date, throws an <see cref="T:System.ArgumentException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument, converted to universal time.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentNullException">DateTime argument is null.</exception>
        /// <exception cref="T:System.ArgumentException">DateTime argument is not a valid date.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotInvalidDate(DateTime argument, string paramName)
        {
            IsNotNull(argument, paramName);

            if (argument.IsValid())
            {
                return;
            }

            throw new ArgumentException($"'{paramName}' is not a valid date.", paramName);
        }

        /// <summary>
        ///     Determines whether the length of specified argument is not greater than the specified length. If greater than the specified length, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="length">The length.</param>
        /// <exception cref="T:System.ArgumentNullException">string argument is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">string argument is more than the specified length.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotOutOfLength(string argument, int length)
        {
            IsNotOutOfLength(argument, length, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the length of specified argument is not greater than the specified length. If greater than the specified length, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="length">The length.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentNullException">string argument is null.</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">string argument is more than the specified length.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design",
            "CA1062:Validate arguments of public methods",
            Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotOutOfLength(string argument, int length, string paramName)
        {
            IsNotNull(argument, paramName);

            if (argument.Trim().Length > length)
            {
                throw new ArgumentOutOfRangeException($"'{paramName}' cannot be more than {length} character(s).",
                    paramName);
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is not below the minimum or above the maximum. If below the minimum or above the maximum, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">int argument must be between min and max.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotOutOfRange(int argument, int min, int max)
        {
            IsNotOutOfRange(argument, min, max, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is not below the minimum or above the maximum. If below the minimum or above the maximum, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="paramName">Name of the argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">int argument must be between min and max.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotOutOfRange(int argument, int min, int max, string paramName)
        {
            if (argument < min || argument > max)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' must be between '{min}' and '{max}'.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">decimal argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(decimal argument)
        {
            IsNotNegative(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">decimal argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(decimal argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">float argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(float argument)
        {
            IsNotNegative(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">float argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(float argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">int argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(int argument)
        {
            IsNotNegative(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">int argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(int argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">long argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(long argument)
        {
            IsNotNegative(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">long argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(long argument, string paramName)
        {
            if (argument < 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">TimeSpan argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(TimeSpan argument)
        {
            IsNotNegative(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative. If negative, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">TimeSpan argument is negative.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegative(TimeSpan argument, string paramName)
        {
            if (argument < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">decimal argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(decimal argument)
        {
            IsNotNegativeOrZero(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">decimal argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(decimal argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative or zero.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">float argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(float argument)
        {
            IsNotNegativeOrZero(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">float argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(float argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative or zero.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">int argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(int argument)
        {
            IsNotNegativeOrZero(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">int argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(int argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative or zero.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">long argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(long argument)
        {
            IsNotNegativeOrZero(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">long argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(long argument, string paramName)
        {
            if (argument <= 0)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative or zero.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">TimeSpan argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(TimeSpan argument)
        {
            IsNotNegativeOrZero(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is negative or zero. If negative or zero, throws an <see cref="T:System.ArgumentOutOfRangeException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentOutOfRangeException">TimeSpan argument is negative or zero.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNegativeOrZero(TimeSpan argument, string paramName)
        {
            if (argument <= TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(paramName, $"'{paramName}' cannot be negative or zero.");
            }
        }

        /// <summary>
        ///     Determines whether the specified argument is <c>null</c>. If <c>null</c>, throws an <see cref="T:System.ArgumentNullException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <exception cref="T:System.ArgumentNullException">object argument is null.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNull(object argument)
        {
            IsNotNull(argument, nameof(argument));
        }

        /// <summary>
        ///     Determines whether the specified argument is <c>null</c>. If <c>null</c>, throws an <see cref="T:System.ArgumentNullException" />; otherwise, returns.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="paramName">Name of the parameter.</param>
        /// <exception cref="T:System.ArgumentNullException">object argument is null.</exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming",
            "CA1704:IdentifiersShouldBeSpelledCorrectly",
            Justification = "Matches argument naming convention in Exception class.", MessageId = "param")]
        public static void IsNotNull(object argument, string paramName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException($"'{paramName}' cannot be null.", paramName);
            }
        }
    }
}