using System;
using System.IO;
using System.Runtime.Serialization;
using Caprica.GOLDEngine.IO;

namespace Caprica.GOLDEngine.Parsing.Exceptions
{
    /// <inheritdoc />
    /// <summary>
    ///     Thrown when a textual expression cannot be parsed.
    ///     For guidelines regarding the creation of new exception types, see
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    ///     and
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    /// </summary>
    [Serializable]
    public class ParserException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Exceptions.ParserException" /> class.
        /// </summary>
        public ParserException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Exceptions.ParserException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ParserException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Exceptions.ParserException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ParserException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Exceptions.ParserException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected ParserException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Exceptions.ParserException" /> class.
        /// </summary>
        /// <param name="type"></param>
        /// <param name="binaryReader"></param>
        public ParserException(EntryType type, BinaryReader binaryReader)
            : base("Type mismatch in file. Read '" + (char) type + "' at " + binaryReader.BaseStream.Position)
        {
        }
    }
}