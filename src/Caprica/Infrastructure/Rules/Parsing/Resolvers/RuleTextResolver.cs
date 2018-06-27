using System;
using Caprica.Extensions;
using Caprica.Helpers;
using Caprica.Infrastructure.Rules.Exceptions;
using Caprica.Infrastructure.Rules.Parsing.Resolvers.Interfaces;

namespace Caprica.Infrastructure.Rules.Parsing.Resolvers
{
    /// <summary>
    ///     TODO
    /// </summary>
    public sealed class RuleTextResolver : IRuleTextResolver
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Parsing.Resolvers.RuleTextResolver" /> class.
        /// </summary>
        public RuleTextResolver()
        {
            Resolver = r => null;
        }

        #region IRuleTextResolver Members

        /// <inheritdoc />
        /// <summary>
        ///     Gets or sets the rule text resolver.
        /// </summary>
        /// <value>
        ///     The rule text resolver.
        /// </value>
        public Func<string, string> Resolver
        {
            private get;
            set;
        }

        /// <summary>
        ///     Resolves the rule text for the specified rule name.
        /// </summary>
        /// <param name="ruleName">The rule name.</param>
        /// <returns>
        ///     The rule text.
        /// </returns>
        public string Resolve(string ruleName)
        {
            Guard.IsNotEmpty(ruleName);

            return Resolve(ruleName, true);
        }

        /// <inheritdoc />
        /// <summary>
        ///     Resolves the rule text for the specified rule name.
        /// </summary>
        /// <param name="ruleName">The rule name.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> throws an exception if the rule is not found.</param>
        /// <returns>
        ///     The rule text.
        /// </returns>
        public string Resolve(string ruleName, bool throwExceptionIfNotFound)
        {
            Guard.IsNotEmpty(ruleName);

            var ruleText = Resolver(ruleName);

            if (ruleText.IsNullOrEmpty() && throwExceptionIfNotFound)
            {
                throw new RuleNotFoundException($"Failed to resolve rule named '{ruleName}'.", ruleName);
            }

            return ruleText;
        }

        #endregion
    }
}