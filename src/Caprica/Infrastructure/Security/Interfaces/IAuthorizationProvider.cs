using System;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Security.Interfaces
{
    /// <summary>
    ///     This interface defines the contract that must be implemented by all authorization providers.
    /// </summary>
    public interface IAuthorizationProvider : IDisposable
    {
        /// <summary>
        ///     Authorizes the specified context against the specified rule text.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The rule authorization context.</param>
        /// <param name="ruleText">The rule text.</param>
        /// <returns>
        ///     <c>true</c> if context's subject is authorized; otherwise, <c>false</c>.
        /// </returns>
        bool Authorize(RuleAuthorizationContext ruleAuthorizationContext, string ruleText);
    }
}