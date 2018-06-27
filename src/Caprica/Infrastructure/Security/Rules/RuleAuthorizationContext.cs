using System.Collections.Generic;
using Caprica.Helpers;

namespace Caprica.Infrastructure.Security.Rules
{
    /// <summary>
    ///     Provides contextual data and services for expression evaluation.
    /// </summary>
    public sealed class RuleAuthorizationContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Security.Rules.RuleAuthorizationContext" /> class.
        /// </summary>
        /// <param name="subject">The subject of authorization.</param>
        public RuleAuthorizationContext(IDictionary<string, object> subject)
        {
            Guard.IsNotNull(subject);

            Subject = subject;
        }

        /// <summary>
        ///     Gets the element at the specified key.
        /// </summary>
        /// <param name="key">The key of the element to get.</param>
        /// <returns>
        ///     The element at the specified key.
        /// </returns>
        public object this[string key] => Subject[key];

        /// <summary>
        ///     Gets or sets the subject.
        /// </summary>
        /// <value>
        ///     The subject.
        /// </value>
        public IDictionary<string, object> Subject
        {
            get;
            private set;
        }
    }
}