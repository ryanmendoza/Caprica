using System;
using Caprica.Infrastructure.Rules.Parsing.Interfaces;

namespace Caprica.Infrastructure.Rules.Parsing
{
    /// <summary>
    /// TODO
    /// </summary>
    public sealed class DefaultRuleLocator : IRuleLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultRuleLocator"/> class.
        /// </summary>
        public DefaultRuleLocator()
        {
            Locator = ruleName => null;
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the rule locater.
        /// </summary>
        /// <value>
        /// The rule locater.
        /// </value>
        public Func<string, string> Locator
        {
            get;
            set;
        }
    }
}