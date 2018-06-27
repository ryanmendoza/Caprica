using System.Collections.Generic;
using System.Linq;
using Caprica.Helpers;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Evaluates to an an enumerable set of object values obtained by evaluating each <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConstantExpression" />
    ///     in the <see cref="T:Caprica.Infrastructure.Rules.Expressions.TupleExpression" />.
    /// </summary>
    public class TupleExpression : Expression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.TupleExpression" /> class.
        /// </summary>
        /// <param name="values">
        ///     The set of <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConstantExpression" /> objects for which evaluated
        ///     results should be returned.
        /// </param>
        public TupleExpression(IEnumerable<ConstantExpression> values)
        {
            Guard.IsNotNull(values);

            Values = values;
        }

        /// <summary>
        ///     Gets the set of <see cref="T:Caprica.Infrastructure.Rules.Expressions.ConstantExpression" /> objects for which
        ///     evaluated results should be returned.
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<ConstantExpression> Values
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
            return string.Join(", ", Values.Select(s => s.ToString()));
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

            var values = Values.Select(s => s.Evaluate(ruleAuthorizationContext));

            return values;
        }
    }
}