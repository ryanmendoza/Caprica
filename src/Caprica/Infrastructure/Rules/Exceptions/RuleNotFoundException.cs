using System;
using System.Runtime.Serialization;
using Caprica.Helpers;

namespace Caprica.Infrastructure.Rules.Exceptions
{
    /// <summary>
    ///     Thrown when an expression referenced by another expression is not found.
    ///     For guidelines regarding the creation of new exception types, see
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/cpgenref/html/cpconerrorraisinghandlingguidelines.asp
    ///     and
    ///     http://msdn.microsoft.com/library/default.asp?url=/library/en-us/dncscol/html/csharp07192001.asp
    /// </summary>
    [Serializable]
    public class RuleNotFoundException : Exception
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.RuleNotFoundException" /> class.
        /// </summary>
        public RuleNotFoundException()
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.RuleNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public RuleNotFoundException(string message)
            : base(message)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.RuleNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The innerException exception.</param>
        public RuleNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.RuleNotFoundException" /> class.
        /// </summary>
        /// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> that holds the serialized object data about the exception being thrown.</param>
        /// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> that contains contextual information about the source or destination.</param>
        /// <exception cref="T:System.ArgumentNullException">The <paramref name="info" /> parameter is null.</exception>
        /// <exception cref="T:System.Runtime.Serialization.SerializationException">The class name is null or <see cref="P:System.Exception.HResult" /> is zero (0).</exception>
        protected RuleNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            Guard.IsNotNull(info);

            RuleName = info.GetString("RuleName");
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Exceptions.RuleNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ruleName">Name of the rule.</param>
        public RuleNotFoundException(string message, string ruleName)
            : this(message, null, ruleName)
        {
        }

        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Rules.Exceptions.RuleNotFoundException" /> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        /// <param name="ruleName">Name of the rule.</param>
        public RuleNotFoundException(string message, Exception innerException, string ruleName)
            : base(message, innerException)
        {
            RuleName = ruleName;
        }

        /// <summary>
        ///     Gets or sets the name of the rule.
        /// </summary>
        /// <value>The name of the rule.</value>
        public string RuleName
        {
            get;
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

            info.AddValue("RuleName", RuleName);

            base.GetObjectData(info, context);
        }
    }
}