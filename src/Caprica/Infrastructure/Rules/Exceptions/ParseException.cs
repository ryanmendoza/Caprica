using System;
using System.Runtime.Serialization;
using Caprica.Helpers;

namespace Caprica.Infrastructure.Rules.Exceptions
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
    public class ParseException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.ParseException" /> class.
        /// </summary>
        public ParseException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.ParseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ParseException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.ParseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public ParseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.ParseException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected ParseException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Guard.IsNotNull(info);

            Column = info.GetInt32("Column");

            Line = info.GetInt32("Line");

            Text = info.GetString("Text");
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.ParseException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="text">The text.</param>
        /// <param name="line">The line.</param>
        /// <param name="column">The column.</param>
        public ParseException(string message, string text, int line, int column)
            : base(message)
        {
            Column = column;

            Line = line;

            Text = text;
        }

        /// <summary>
        ///     Gets the column.
        /// </summary>
        public int Column
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the line.
        /// </summary>
        public int Line
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the text.
        /// </summary>
        public string Text
        {
            get;
            private set;
        }

        /// <inheritdoc />
        /// <summary>
        ///     When overridden in a derived class, sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> with information about the exception.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <PermissionSet>
        ///     <IPermission class="System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Read="*AllFiles*" PathDiscovery="*AllFiles*" />
        ///     <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="SerializationFormatter" />
        /// </PermissionSet>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            Guard.IsNotNull(info);

            info.AddValue("Column", Column);

            info.AddValue("Line", Line);

            info.AddValue("Text", Text);

            base.GetObjectData(info, context);
        }
    }
}