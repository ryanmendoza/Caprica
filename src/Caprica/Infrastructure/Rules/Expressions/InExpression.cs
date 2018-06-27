using System;
using System.Collections.Generic;
using System.Linq;
using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Evaluates whether a value exists in a set of values.
    /// </summary>
    public class InExpression : BooleanExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.InExpression" /> class.
        /// </summary>
        /// <param name="value">
        ///     A <see cref="T:Caprica.Infrastructure.Rules.Expressions.VariableExpression" /> representing the
        ///     value to be matched.
        /// </param>
        /// <param name="set">
        ///     A <see cref="T:Caprica.Infrastructure.Rules.Expressions.MultiValueExpression" /> representing the
        ///     values to be evaluated for <paramref name="value" />.
        /// </param>
        public InExpression(VariableExpression value, TupleExpression set)
        {
            Guard.IsNotNull(value);

            Guard.IsNotNull(set);

            Set = set;

            Value = value;
        }

        /// <summary>
        ///     Gets the <see cref="T:Caprica.Infrastructure.Rules.Expressions.TupleExpression" /> representing
        ///     the values to be evaluated for <see cref="Value" />.
        /// </summary>
        /// <value>The set.</value>
        public TupleExpression Set
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the <see cref="T:Caprica.Infrastructure.Rules.Expressions.VariableExpression" /> representing the
        ///     value to be matched.
        /// </summary>
        /// <value>The value.</value>
        public ValueExpression Value
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
            return $"{Value} IN ({Set})";
        }

        /// <summary>
        ///     Evaluates the expression in the specified context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The context in which the expression should be evaluated.</param>
        /// <returns>
        ///     The result of evaluation.
        /// </returns>
        /// <remarks>
        ///     <see cref="T:Caprica.Infrastructure.Rules.Expressions.InExpression" /> will always return a
        ///     <see cref="T:System.Boolean" /> result.
        /// </remarks>
        /// <exception cref="T:Caprica.Infrastructure.Rules.Exceptions.EvaluationException">
        ///     The data types of <see cref="Value" /> and <see cref="Set" /> are not compatible for comparison.
        /// </exception>
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var value = (IComparable) Value.Evaluate(ruleAuthorizationContext);

            var setValues = (IEnumerable<object>) Set.Evaluate(ruleAuthorizationContext);

            return setValues.Any(s => value.CompareTo(s) == 0);
        }
    }
}