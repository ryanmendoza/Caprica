using System;

namespace Caprica.Infrastructure.Rules.Parsing.Interfaces
{
    public interface IRuleLocator
    {
        /// <summary>
        /// Gets or sets the rule locater.
        /// </summary>
        /// <value>
        /// The rule locater.
        /// </value>
        Func<string, string> Locator
        {
            get;
            set;
        }
    }
}