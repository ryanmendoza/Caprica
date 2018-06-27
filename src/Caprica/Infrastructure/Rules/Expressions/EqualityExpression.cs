using Caprica.Helpers;
using Caprica.Infrastructure.Rules.Exceptions;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Evaluates the equality of one value to another value given an equality operator.
    /// </summary>
    public class EqualityExpression : BooleanExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.EqualityExpression" /> class.
        /// </summary>
        /// <param name="left">The expression on the left of the operator.</param>
        /// <param name="operator">
        ///     A <see cref="T:Caprica.Infrastructure.Rules.Expressions.EqualityOperator" /> representing the
        ///     equality comparison to be performed.
        /// </param>
        /// <param name="right">The expression on the right of the operator.</param>
        public EqualityExpression(ValueExpression left, EqualityOperator @operator, ValueExpression right)
        {
            Guard.IsNotNull(left);

            Guard.IsNotNull(@operator);

            Guard.IsNotNull(right);

            Left = left;

            Operator = @operator;

            Right = right;
        }

        /// <summary>
        ///     Gets the value expression on the left of the operator.
        /// </summary>
        /// <value>The left.</value>
        public ValueExpression Left
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets a <see cref="T:Caprica.Infrastructure.Rules.Expressions.EqualityOperator" /> representing the equality comparison to be performed.
        /// </summary>
        /// <value>The operator.</value>
        public EqualityOperator Operator
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the value expression on the right of the operator.
        /// </summary>
        /// <value>The right.</value>
        public ValueExpression Right
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
            return $"{Left} {Operator} {Right}";
        }

        /// <summary>
        ///     Evaluates the expression in the specified context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The context in which the expression should be evaluated.</param>
        /// <returns>
        ///     The result of evaluation.
        /// </returns>
        /// <remarks>
        ///     <see cref="T:Caprica.Infrastructure.Rules.Expressions.EqualityExpression" /> will always return a <see cref="T:System.Boolean" /> result.
        /// </remarks>
        /// <exception cref="T:Caprica.Infrastructure.Rules.Exceptions.EvaluationException">
        ///     The data types of <see cref="Left" /> and <see cref="Right" /> are not compatible for comparison.
        /// </exception>
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var leftValue = Left.Evaluate(ruleAuthorizationContext);

            var rightValue = Right.Evaluate(ruleAuthorizationContext);

            switch (Operator)
            {
                case EqualityOperator.EqualTo:
                    return leftValue.Equals(rightValue);

                case EqualityOperator.NotEqualTo:
                    return !leftValue.Equals(rightValue);
            }

            throw new EvaluationException($"An unknown EqualityOperator was encountered. Found '{Operator}'. Expected 'EqualityOperator.EqualTo' or 'EqualityOperator.NotEqualTo'.");
        }
    }
}