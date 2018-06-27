using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Evaluates to a constant value.
    /// </summary>
    public class ConstantExpression : ValueExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConstantExpression" /> class.
        /// </summary>
        /// <param name="value">The value to be returned as the evaluation result.</param>
        public ConstantExpression(object value)
        {
            Guard.IsNotNull(value);

            Value = value;
        }

        /// <summary>
        ///     Gets the value to be returned as the evaluation result.
        /// </summary>
        /// <value>The value.</value>
        public object Value { get; private set; }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return Value is string ? "\"" + Value + "\"" : Value.ToString();
        }

        /// <summary>
        ///     Evaluates the expression in the specified context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The context in which the expression should be evaluated.</param>
        /// <returns>
        ///     The result of evaluation.
        /// </returns>
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            return Value;
        }
    }
}