namespace Caprica.Infrastructure.Rules.Expressions
{
    /// <summary>
    ///     Represents an operator to be used for comparing equality of values.
    /// </summary>
    public enum EqualityOperator
    {
        /// <summary>
        ///     The value on the left side of the operator
        ///     is equal to the value on the right side of the operator.
        /// </summary>
        EqualTo,

        /// <summary>
        ///     The value on the left side of the operator
        ///     is not equal to the value on the right side of the operator.
        /// </summary>
        NotEqualTo
    }
}