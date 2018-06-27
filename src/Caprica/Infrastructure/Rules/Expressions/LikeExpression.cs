using System;
using System.Globalization;
using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Evalutes whether a value matches a pattern.
    /// </summary>
    public class LikeExpression : BooleanExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.LikeExpression" /> class.
        /// </summary>
        /// <param name="value">
        ///     The <see cref="T:Caprica.Infrastructure.Rules.Expressions.VariableExpression" />
        ///     to match against <paramref name="pattern" />.
        /// </param>
        /// <param name="pattern">
        ///     An <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConstantExpression" /> representing
        ///     the pattern against which <paramref name="value" /> will be matched.
        /// </param>
        public LikeExpression(VariableExpression value, ConstantExpression pattern)
        {
            Guard.IsNotNull(value);

            Guard.IsNotNull(pattern);

            Pattern = pattern;

            Value = value;
        }

        /// <summary>
        ///     Gets the <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConstantExpression" /> against which
        ///     the value represented by <see cref="Value" /> will be matched.
        /// </summary>
        /// <value>The pattern.</value>
        public ConstantExpression Pattern
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the <see cref="T:Caprica.Infrastructure.Rules.Expressions.VariableExpression" /> to match
        ///     against <see cref="Pattern" />.
        /// </summary>
        /// <value>The match expression.</value>
        public VariableExpression Value
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
            return $"{Value} LIKE \"{Pattern}\"";
        }

        /// <summary>
        ///     Evaluates the expression in the specified context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The context in which the expression should be evaluated.</param>
        /// <returns>
        ///     The result of evaluation.
        /// </returns>
        /// <remarks>
        ///     <see cref="T:Caprica.Infrastructure.Rules.Expressions.LikeExpression" />s will always return a
        ///     <see cref="T:System.Boolean" /> result.
        /// </remarks>
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var patternValue = Pattern.Evaluate(ruleAuthorizationContext).ToString().ToUpper(CultureInfo.InvariantCulture);

            var valueValue = Value.Evaluate(ruleAuthorizationContext).ToString().ToUpper(CultureInfo.InvariantCulture);

            var barePattern = patternValue.Trim().Trim('%');

            // Starts with wildcard...
            if (patternValue.StartsWith("%", StringComparison.OrdinalIgnoreCase))
            {
                // ... and ends with wildcard; match within the string.
                if (patternValue.EndsWith("%", StringComparison.OrdinalIgnoreCase))
                {
                    return valueValue.Contains(barePattern);
                }

                // ... but does not end with wildcard; match the end of the string.
                return valueValue.EndsWith(barePattern, StringComparison.OrdinalIgnoreCase);
            }

            if (patternValue.EndsWith("%", StringComparison.OrdinalIgnoreCase))
            {
                // Ends with wildcard; match the start of the string.
                return valueValue.StartsWith(barePattern, StringComparison.OrdinalIgnoreCase);
            }

            // No wildcards; we'll do a full match.
            return string.Compare(valueValue, patternValue, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}