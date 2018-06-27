namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <summary>
    ///     Represents an operator to be used for comparing relations of values.
    /// </summary>
    public enum RelationalOperator
    {
        /// <summary>
        ///     The value on the left side of the operator
        ///     is less than the value on the right side of the operator.
        /// </summary>
        LessThan,

        /// <summary>
        ///     The value on the left side of the operator
        ///     is less than or equal to the value on the right side
        ///     of the operator.
        /// </summary>
        LessThanOrEqual,

        /// <summary>
        ///     The value on the left side of the operator
        ///     is greater than the value on the right side of the operator.
        /// </summary>
        GreaterThan,

        /// <summary>
        ///     The value on the left side of the operator
        ///     is greater than or equal to the value on the right side
        ///     of the operator.
        /// </summary>
        GreaterThanOrEqual
    }
}