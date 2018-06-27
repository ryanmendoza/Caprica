using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
     /// <inheritdoc />
     /// <summary>
     ///     Evaluates whether two expressions are both <see langword="true" />.
     /// </summary>
    public class ConditionalAndExpression : BooleanExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConditionalAndExpression" /> class.
        /// </summary>
        /// <param name="left">
        ///     The left <see cref="T:Caprica.Infrastructure.Rules.Expressions.Expression" /> to be evaluated.
        /// </param>
        /// <param name="right">
        ///     The right <see cref="T:Caprica.Infrastructure.Rules.Expressions.Expression" /> to be evaluted.
        /// </param>
        public ConditionalAndExpression(Expression left, Expression right)
        {
            Guard.IsNotNull(left);

            Guard.IsNotNull(right);

            Left = left;

            Right = right;
        }

        /// <summary>
        ///     Gets the left value to be evaluated.
        /// </summary>
        /// <value>The left value.</value>
        public Expression Left
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the right value to be evaluated.
        /// </summary>
        /// <value>The right value.</value>
        public Expression Right
        {
            get;
            private set;
        }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "{Left} AND {Right}";
        }

        /// <summary>
        ///     Evaluates the expression in the specified context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The context in which the expression should be evaluated.</param>
        /// <returns>
        ///     The result of evaluation.
        /// </returns>
        /// <remarks>
        ///     <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConditionalAndExpression" /> will always
        ///     return a <see cref="T:System.Boolean" /> result.
        /// </remarks>
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var left = (bool) Left.Evaluate(ruleAuthorizationContext);

            var right = (bool) Right.Evaluate(ruleAuthorizationContext);

            return left && right;
        }
    }
}