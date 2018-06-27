using System;
using System.Linq;
using Caprica.Extensions;
using Caprica.Helpers;
using Caprica.Infrastructure.Rules.Exceptions;
using Caprica.Infrastructure.Security.Rules;

namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <inheritdoc />
    /// <summary>
    ///     Evalutes to the value of an input variable.
    /// </summary>
    public class VariableExpression : ValueExpression
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Expressions.VariableExpression" /> class.
        /// </summary>
        /// <param name="variableName">The variable name.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        public VariableExpression(string variableName)
        {
            Guard.IsNotEmpty(variableName);

            var variableNameAsArray = variableName.Split(new[] { '.' }, StringSplitOptions.RemoveEmptyEntries);

            PropertyName = variableNameAsArray.Length == 2 ? variableNameAsArray.Last() : null;

            VariableName = variableNameAsArray.First();
        }

        /// <summary>
        ///     Gets the property.
        /// </summary>
        /// <value>
        ///     The property.
        /// </value>
        public string PropertyName
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the name of the input variable whose value
        ///     will be returned.
        /// </summary>
        /// <value>The name of the variable.</value>
        public string VariableName
        {
            get;
            private set;
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var propertyName = PropertyName;

            var variableName = VariableName;

            return propertyName.IsNullOrEmpty() ? variableName : $"{variableName}.{propertyName}";
        }

        /// <inheritdoc />
        /// <summary>
        ///     Evaluates the expression in the specified context.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The context in which the expression should be evaluated.</param>
        /// <returns>
        ///     The result of evaluation.
        /// </returns>
        /// <remarks>
        ///     The variable must exist in <paramref name="ruleAuthorizationContext" />.
        /// </remarks>
        /// <exception cref="T:Caprica.Infrastructure.Rules.Expressions.VariableNotFoundException">
        ///     No variable with a name matching <see cref="P:Caprica.Infrastructure.Rules.Expressions.VariableExpression.VariableName" /> was found in <paramref name="ruleAuthorizationContext" />.
        /// </exception>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        protected override object EvaluateImpl(RuleAuthorizationContext ruleAuthorizationContext)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            var variableName = VariableName.Substring(1); // Trim off the @ for indexer.

            var variableValue = ruleAuthorizationContext[variableName];

            if (variableValue == null)
            {
                throw new VariableNotFoundException($"A variable named '{variableName}' was encountered in the expression, but no variable by that name exists in the current rule authorization context.");
            }

            var propertyName = PropertyName;

            return propertyName.IsNullOrEmpty() ? variableValue : GetPropertyValue(variableValue, propertyName);
        }

        private static object GetPropertyValue(object obj, string propertyName)
        {
            var propertyInfo = obj.GetType().GetProperty(propertyName);

            return propertyInfo?.GetValue(obj, null);
        }
    }
}