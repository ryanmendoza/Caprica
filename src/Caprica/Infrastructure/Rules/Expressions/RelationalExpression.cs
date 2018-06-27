using System;
using Caprica.Helpers;
using Caprica.Infrastructure.Rules.Exceptions;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Evaluates the equality of one value to another value given an equality operator.
    /// </summary>
    public class RelationalExpression : BooleanExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.RelationalExpression" /> class.
        /// </summary>
        /// <param name="left">The expression on the left of the operator.</param>
        /// <param name="operator">
        ///     A <see cref="T:Caprica.Infrastructure.Rules.Expressions.RelationalOperator" /> representing the
        ///     relational comparison to be performed.
        /// </param>
        /// <param name="right">The expression on the right of the operator.</param>
        public RelationalExpression(ValueExpression left, RelationalOperator @operator, ValueExpression right)
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
        ///     Gets a <see cref="T:Caprica.Infrastructure.Rules.Expressions.RelationalOperator" /> representing the relational comparison to be performed.
        /// </summary>
        /// <value>The operator.</value>
        public RelationalOperator Operator
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
        ///     <see cref="T:Caprica.Infrastructure.Rules.Expressions.RelationalExpression" /> will always return a <see cref="T:System.Boolean" /> result.
        /// </remarks>
        /// <exception cref="T:Caprica.Infrastructure.Rules.Exceptions.EvaluationException">
        ///     The data types of <see cref="Left" /> and <see cref="Right" /> are not compatible for comparison.
        /// </exception>
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var leftValue = (IComparable) Left.Evaluate(ruleAuthorizationContext);

            var rightValue = Right.Evaluate(ruleAuthorizationContext);

            switch (Operator)
            {
                case RelationalOperator.GreaterThan:
                    return leftValue.CompareTo(rightValue) > 0;

                case RelationalOperator.GreaterThanOrEqual:
                    return leftValue.CompareTo(rightValue) >= 0;

                case RelationalOperator.LessThan:
                    return leftValue.CompareTo(rightValue) < 0;

                case RelationalOperator.LessThanOrEqual:
                    return leftValue.CompareTo(rightValue) <= 0;
            }

            throw new EvaluationException($"An unknown RelationalOperator was encountered. Found '{Operator}'. Expected 'RelationalOperator.GreaterThan', 'RelationalOperator.GreaterThanOrEqual', 'RelationalOperator.LessThan' or 'RelationalOperator.LessThanOrEqual'.");
        }
    }
}