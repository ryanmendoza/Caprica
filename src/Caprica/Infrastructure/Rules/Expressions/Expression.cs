using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <summary>
    ///     An evaluatable expression.
    /// </summary>
    public abstract class Expression
    {
        /// <summary>
        ///     Evaluates the expression in the specified evaluation context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The evaluation context in which the expression should be evaluated.</param>
        /// <returns>The result of the expression evaluation.</returns>
        public object Evaluate(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var result = EvaluateImpl(ruleAuthorizationContext);

            return result;
        }

        /// <summary>
        ///     Evaluates the expression in the specified evaluation context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The evaluation ontext in which the expression should be evaluated.</param>
        /// <returns>The result of the expression evaluation.</returns>
        protected abstract object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext);
    }
}