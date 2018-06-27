using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace Caprica.Extensions
{
    /// <summary>
    ///     Defines an static class which contains extension methods of <see cref="T:System.Exception" />.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        ///     Determines whether the specified target exception is fatal.
        /// </summary>
        /// <param name="target">The target exception.</param>
        /// <returns>
        ///     <c>true</c> if the specified target exception is fatal; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsFatal(this Exception target)
        {
            for (var innerException = target; innerException != null; innerException = innerException.InnerException)
            {
                if (!(innerException is InsufficientMemoryException) && innerException is OutOfMemoryException || innerException is AccessViolationException || innerException is SEHException || innerException is ThreadAbortException)
                {
                    return true;
                }
            }

            return false;
        }
    }
}