namespace Caprica.Infrastructure.Rules.Helpers
{
    internal static class Constants
    {
        #region Nested type: Nonterminals

        /// <summary>
        /// </summary>
        public static class Nonterminals
        {
            /// <summary>
            /// </summary>
            public const string BooleanExpression = "Boolean Expression";

            /// <summary>
            /// </summary>
            public const string ConditionalAndExpression = "Conditional AND Expression";

            /// <summary>
            /// </summary>
            public const string ConditionalOrExpression = "Conditional OR Expression";

            /// <summary>
            /// </summary>
            public const string DateConstantExpression = "Date Constant Expression";

            /// <summary>
            /// </summary>
            public const string DateTupleExpression = "Date Tuple Expression";

            /// <summary>
            /// </summary>
            public const string EqualityExpression = "Equality Expression";

            /// <summary>
            /// </summary>
            public const string GroupExpression = "Group Expression";

            /// <summary>
            /// </summary>
            public const string InExpression = "In Expression";

            /// <summary>
            /// </summary>
            public const string LikeExpression = "Like Expression";

            /// <summary>
            /// </summary>
            public const string NumericConstantExpression = "Numeric Constant Expression";

            /// <summary>
            /// </summary>
            public const string NumericTupleExpression = "Numeric Tuple Expression";

            /// <summary>
            /// </summary>
            public const string ReferenceExpression = "Reference Expression";

            /// <summary>
            /// </summary>
            public const string RelationalExpression = "Relational Expression";

            /// <summary>
            /// </summary>
            public const string StringConstantExpression = "String Constant Expression";

            /// <summary>
            /// </summary>
            public const string StringTupleExpression = "String Tuple Expression";

            /// <summary>
            /// </summary>
            public const string UnaryExpression = "Unary Expression";

            /// <summary>
            /// </summary>
            public const string ValueConstantExpression = "Value Constant Expression";

            /// <summary>
            /// </summary>
            public const string VariableExpression = "Variable Expression";
        }

        #endregion

        #region Nested type: Terminals

        /// <summary>
        /// </summary>
        public static class Terminals
        {
            public const string LogicalNegationOperator = "!";

            public const string InequalityOperator = "!=";

            public const string ConditionalAndOperator = "&&";

            public const string LeftParenthesis = "(";

            public const string RightParenthesis = ")";

            public const string Comma = ",";

            public const string ConditionalOrOperator = "||";

            public const string LessThanRelationalOperator = "<";

            public const string LessThanOrEqualToRelationalOperator = "<=";

            public const string EqualityOperator = "==";

            public const string GreaterThanRelationalOperator = ">";

            public const string GreaterThanOrEqualToRelationalOperator = ">=";

            public const string Date = "Date";

            public const string In = "IN";

            public const string Integer = "Integer";

            public const string Like = "LIKE";

            public const string Real = "Real";

            public const string Reference = "Reference";

            public const string String = "String";

            public const string Variable = "Variable";
        }

        #endregion
    }
}