using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     The logical negation operator (!) is a unary operator that negates its operand. It returns <see langword="true" /> if and only if its operand is <see langword="false" />.
    /// </summary>
    public class UnaryExpression : BooleanExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.UnaryExpression" /> class.
        /// </summary>
        /// <param name="expression">
        ///     The <see cref="T:Caprica.Infrastructure.Rules.Expressions.BooleanExpression" /> to be evaluated and negated.
        /// </param>
        public UnaryExpression(BooleanExpression expression)
        {
            Guard.IsNotNull(expression);

            Expression = expression;
        }

        /// <summary>
        ///     Gets the <see cref="T:Caprica.Infrastructure.Rules.Expressions.BooleanExpression" /> to be evaluated and negated.
        /// </summary>
        /// <value>The expression.</value>
        public BooleanExpression Expression { get; private set; }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return $"NOT ({Expression})";
        }

        /// <summary>
        ///     Evaluates the expression in the specified context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The context in which the expression should be evaluated.</param>
        /// <returns>
        ///     The result of evaluation.
        /// </returns>
        /// <remarks>
        ///     <see cref="T:Caprica.Infrastructure.Rules.Expressions.UnaryExpression" /> will always return a
        ///     <see cref="T:System.Boolean" /> result.
        /// </remarks>
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var expression = (bool) Expression.Evaluate(ruleAuthorizationContext);

            return !expression;
        }
    }
}