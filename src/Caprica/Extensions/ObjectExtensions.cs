namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Object" />.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        ///     TODO
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type" /> to cast to.</typeparam>
        /// <param name="target">The target object to cast.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static T As<T>(this object target) where T : class
        {
            return target as T;
        }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <typeparam name="T">The <see cref="T:System.Type" /> to cast to.</typeparam>
        /// <param name="target">The target object to cast.</param>
        /// <returns>
        ///     TODO
        /// </returns>
        public static T To<T>(this object target)
        {
            if (target is T)
            {
                return (T) target;
            }

            return default(T);
        }

        /// <summary>
        ///     Safe guards the target string from being <c>null</c> by converting it to an empty string if in fact it is <c>null</c>.
        /// </summary>
        /// <param name="target">The target object to safe guard.</param>
        /// <returns>
        ///     The trimmed target string; otherwise, an empty string ("").
        /// </returns>
        public static string ToNullSafeString(this object target)
        {
            return target.ToNullSafeString(string.Empty);
        }

        /// <summary>
        ///     Safe guards the target string from being <c>null</c> by converting it to an empty string if in fact it is <c>null</c>.
        /// </summary>
        /// <param name="target">The target object to safe guard.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>
        ///     The trimmed target string; otherwise, an empty string ("").
        /// </returns>
        public static string ToNullSafeString(this object target, string defaultValue)
        {
            var targetAsAString = (target ?? string.Empty).ToString();

            return string.IsNullOrWhiteSpace(targetAsAString) ? defaultValue : targetAsAString;
        }
    }
}