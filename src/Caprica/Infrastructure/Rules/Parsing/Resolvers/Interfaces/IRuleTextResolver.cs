using System;

namespace Caprica.Infrastructure.Rules.Parsing.Resolvers.Interfaces
{
    public interface IRuleTextResolver
    {
        /// <summary>
        ///     Gets or sets the rule text resolver.
        /// </summary>
        /// <value>
        ///     The rule text resolver.
        /// </value>
        Func<string, string> Resolver
        {
            set;
        }
        
        /// <summary>
        ///     Resolves the rule text for the specified rule name.
        /// </summary>
        /// <param name="ruleName">The rule name.</param>
        /// <returns>
        ///     The rule text.
        /// </returns>
        string Resolve(string ruleName);

        /// <summary>
        /// Resolves the rule text for the specified rule name.
        /// </summary>
        /// <param name="ruleName">The rule name.</param>
        /// <param name="throwExceptionIfNotFound">if set to <c>true</c> throws an exception if the rule is not found.</param>
        /// <returns>
        /// The rule text.
        /// </returns>
        string Resolve(string ruleName, bool throwExceptionIfNotFound);
    }
}