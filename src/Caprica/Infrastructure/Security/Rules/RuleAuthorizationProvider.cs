using System;
using System.Collections.Generic;
using System.Linq;
using Caprica.Abstractions;
using Caprica.Extensions;
using Caprica.Helpers;
using Caprica.Infrastructure.Rules.Exceptions;
using Caprica.Infrastructure.Rules.Expressions;
using Caprica.Infrastructure.Rules.Parsing;
using Caprica.Infrastructure.Security.Interfaces;

namespace Caprica.Infrastructure.Security.Rules
{
    /// <summary>
    ///     An authorization provider whose decision is based on rules (expressions).
    /// </summary>
    public sealed class RuleAuthorizationProvider : Disposable, IAuthorizationProvider
    {
        private readonly ExpressionBuilder _expressionBuilder;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Security.Rules.RuleAuthorizationProvider" /> class.
        /// </summary>
        /// <param name="expressionBuilder">The expression builder.</param>
        public RuleAuthorizationProvider(ExpressionBuilder expressionBuilder)
        {
            Guard.IsNotNull(expressionBuilder);

            _expressionBuilder = expressionBuilder;
        }

        #region IAuthorizationProvider Members

        /// <summary>
        ///     Authorizes the specified context against the specified rule text.
        /// </summary>
        /// <param name="ruleAuthorizationContext">The rule authorization context.</param>
        /// <param name="ruleText">The rule text.</param>
        /// <returns>
        ///     <c>true</c> if context's subject is authorized; otherwise, <c>false</c>.
        /// </returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", Justification = "Arguments are validated using a resuable Guard helper.", MessageId = "1")]
        public bool Authorize(RuleAuthorizationContext ruleAuthorizationContext, string ruleText)
        {
            Guard.IsNotNull(ruleAuthorizationContext);

            if (string.IsNullOrWhiteSpace(ruleText))
            {
                return true;
            }

            try
            {
                var expression = _expressionBuilder.Parse(ruleText);

                try
                {
                    if (expression is BooleanExpression)
                    {
                        var isAuthorized = (bool) expression.Evaluate(ruleAuthorizationContext);

                        return isAuthorized;
                    }
                }
                catch (Exception exception)
                {
                    throw new EvaluationException("The expression could not be evaluated.\r\nSubject:\r\n{0}\r\nRule Text ({1}): {2}\r\nParsed Expression: {3}\r\n".FormatWith(string.Join("\r\n", ruleAuthorizationContext.Subject.Select(kvp => "{0}: {1}".FormatWith(kvp.Key, kvp.Value is IEnumerable<object> ? string.Join(", ", ((IEnumerable<object>)kvp.Value).Select(v => string.IsNullOrWhiteSpace(v.ToNullSafeString()) ? "null" : v)) : (string.IsNullOrWhiteSpace(kvp.Value.ToNullSafeString()) ? "null" : kvp.Value)))), ruleText.Length, ruleText, expression.ToString()), exception);
                }

                throw new EvaluationException("The parsed expression is of type '{0}'. An expression of type '{1}' was expected.\r\n".FormatWith(expression.GetType().FullName, typeof(BooleanExpression).FullName));
            }
            catch (Exception exception)
            {
                throw new ParseException("The rule text could not be parsed.\r\nSubject:\r\n{0}\r\nRule Text ({1}): {2}\r\n".FormatWith(string.Join("\r\n", ruleAuthorizationContext.Subject.Select(kvp => "{0}: {1}".FormatWith(kvp.Key, kvp.Value is IEnumerable<object> ? string.Join(", ", ((IEnumerable<object>)kvp.Value).Select(v => string.IsNullOrWhiteSpace(v.ToNullSafeString()) ? "null" : v)) : (string.IsNullOrWhiteSpace(kvp.Value.ToNullSafeString()) ? "null" : kvp.Value)))), ruleText.Length, ruleText), exception);
            }
        }

        #endregion

        /// <summary>
        ///     Disposes the resources of this <see cref="T:Caprica.Abstractions.Disposable" /> instance.
        /// </summary>
        protected override void DisposeCore()
        {
            _expressionBuilder.Dispose();
        }
    }
}