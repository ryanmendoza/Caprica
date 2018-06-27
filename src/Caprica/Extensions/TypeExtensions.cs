using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Caprica.Helpers;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="Type" />.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        ///     Gets the concretes the types of the specified assembly.
        /// </summary>
        /// <param name="instance">The assembly to get concrete types for.</param>
        /// <returns>
        ///     A collection of concrete types for the specified assembly.
        /// </returns>
        public static IEnumerable<Type> ConcreteTypes(this Assembly instance)
        {
            return instance == null ?
                Enumerable.Empty<Type>() :
                instance.PublicTypes()
                        .Where(t => t != null && t.IsClass && !t.IsAbstract && !t.IsInterface && !t.IsGenericType);
        }

        /// <summary>
        ///     Gets the concretes the types of the specified assemblies.
        /// </summary>
        /// <param name="instance">The assemblies to get concrete types for.</param>
        /// <returns>
        ///     A collection of concrete types for the specified assemblies.
        /// </returns>
        public static IEnumerable<Type> ConcreteTypes(this IEnumerable<Assembly> instance)
        {
            return instance == null ?
                Enumerable.Empty<Type>() :
                instance.SelectMany(a => a.ConcreteTypes()).OrderByDescending(ks => ks.Name);
        }

        /// <summary>
        ///     Determines whether the specified type has parameterless contructor.
        /// </summary>
        /// <param name="instance">The instance to check.</param>
        /// <returns>
        ///     <c>true</c> if parameterless constructor exists; otherwise, <c>false</c>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0")]
        public static bool HasDefaultConstructor(this Type instance)
        {
            Guard.IsNotNull(instance);

            return instance.GetConstructors(BindingFlags.Instance | BindingFlags.Public).Any(c => c.GetParameters().Length.Equals(0));
        }

        /// <summary>
        ///     Determines whether the specified type is nullable.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>
        ///     <c>true</c> if the specified type is nullable; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullable(this Type type)
        {
            Guard.IsNotNull(type);

            return type.IsValueType && Nullable.GetUnderlyingType(type) != null;
        }

        /// <summary>
        ///     Gets the public types of the specified assembly.
        /// </summary>
        /// <param name="instance">The assembly to get public types for.</param>
        /// <returns>
        ///     A collection of public types for the specified assembly.
        /// </returns>
        public static IEnumerable<Type> PublicTypes(this Assembly instance)
        {
            if (instance == null)
            {
                return Enumerable.Empty<Type>();
            }

            IEnumerable<Type> types;

            try
            {
                types = instance.GetTypes().Where(t => t != null && t.IsPublic && t.IsVisible);
            }
            catch (ReflectionTypeLoadException reflectionTypeLoadException)
            {
                types = reflectionTypeLoadException.Types;
            }

            return types ?? Enumerable.Empty<Type>();
        }

        /// <summary>
        ///     Gets the public types of the specified assemblies.
        /// </summary>
        /// <param name="instance">The collection of assemblies to get public types for.</param>
        /// <returns>
        ///     A collection of public types for the specified assemblies.
        /// </returns>
        public static IEnumerable<Type> PublicTypes(this IEnumerable<Assembly> instance)
        {
            return instance == null ?
                Enumerable.Empty<Type>() :
                instance.SelectMany(a => a.PublicTypes());
        }
    }
}