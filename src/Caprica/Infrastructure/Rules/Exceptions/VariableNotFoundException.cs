using System;
using System.Runtime.Serialization;

namespace Caprica.Infrastructure.Rules.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Thrown when a variable necessary for expression evaluation is not found.
    ///     For guidelines regarding the creation of new exception types, see
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    ///     and
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    /// </summary>
    [Serializable]
    public class VariableNotFoundException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.VariableNotFoundException" /> class.
        /// </summary>
        public VariableNotFoundException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.VariableNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public VariableNotFoundException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.VariableNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public VariableNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.VariableNotFoundException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected VariableNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}