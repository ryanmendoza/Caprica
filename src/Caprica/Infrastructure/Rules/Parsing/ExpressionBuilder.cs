using Caprica.GOLDEngine.Parsing;

namespace Caprica.Infrastructure.Rules.Parsing
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Abstractions;
    using Caprica.Helpers;
    using Exceptions;
    using Expressions;
    using Extensions;
    using Caprica.Infrastructure.Rules.Parsing.Resolvers.Interfaces;
    using C = Helpers.Constants;

    #endregion

    /// <inheritdoc />
    /// <summary>
    ///     TODO
    /// </summary>
    public sealed class ExpressionBuilder : Disposable
    {
        private readonly IRuleTextResolver _ruleTextResolver;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.Infrastructure.Rules.Parsing.ExpressionBuilder" /> class.
        /// </summary>
        /// <param name="ruleTextResolver">The rule text resolver.</param>
        public ExpressionBuilder(IRuleTextResolver ruleTextResolver)
        {
            Guard.IsNotNull(ruleTextResolver);

            _ruleTextResolver = ruleTextResolver;
        }

        /// <summary>
        ///     Parses the specified rule text into an expression.
        /// </summary>
        /// <param name="ruleText">The rule text.</param>
        /// <returns>
        ///     The expression.
        /// </returns>
        public Expression Parse(string ruleText)
        {
            using (var parser = new Parser())
            {
                parser.LoadTables(GetType(), "Caprica.Infrastructure.Rules.Grammar.Rules.egt");

                parser.Open(ruleText);

                while (true)
                {
                    switch (parser.Parse())
                    {
                        case ParseMessageType.Accept:
                            return Visit(parser.CurrentReduction);

                        case ParseMessageType.GroupError:
                            throw new ParseException("Syntax Error: Malformed Group");

                        case ParseMessageType.InternalError:
                            throw new ParseException("Internal Error");

                        case ParseMessageType.LexicalError:
                            throw new ParseException($"Lexical Error: Encountered unknown token.\r\nPosition: Line {parser.CurrentInputToken.Position.Line}, Column {parser.CurrentInputToken.Position.Column}\r\nFound: {parser.CurrentInputToken.Data}\r\nRule Text ({parser.RuleText}):\r\n'{parser.RuleText.Length}'");

                        case ParseMessageType.NotLoadedError:
                            throw new ParseException("Enhanced Grammar Tables (EGT) not loaded");

                        case ParseMessageType.Reduction:
                            break;

                        case ParseMessageType.SyntaxError:
                            throw new ParseException($"Syntax Error:\r\nPosition: Line {parser.CurrentInputToken.Position.Line}, Column {parser.CurrentInputToken.Position.Column}\r\nFound: {parser.CurrentInputToken.Data}\r\nExpecting the following tokens:\r\n{string.Join(", ", parser.ExpectedSymbols.Select(expectedSymbol => expectedSymbol.ToString()))}\r\nRule Text ({parser.RuleText.Length}):\r\n'{parser.RuleText}'");

                        case ParseMessageType.TokenRead:
                            break;
                    }
                }
            }
        }

        private static ConstantExpression VisitConstant(object data)
        {
            Guard.IsNotNull(data);

            return new ConstantExpression(data);
        }

        private static ConstantExpression VisitStringConstant(Token node)
        {
            Guard.IsNotNull(node);

            return VisitConstant(node.Trim());
        }

        private static ConstantExpression VisitDateConstant(Token node)
        {
            Guard.IsNotNull(node);

            var value = DateTime.ParseExact(node.Trim(), "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None);

            return VisitConstant(value);
        }

        private static ConstantExpression VisitNumericConstant(Token node)
        {
            Guard.IsNotNull(node);

            switch (node.Parent.Name)
            {
                case C.Terminals.Integer:
                    return VisitConstant(int.Parse(node.Trim(), NumberStyles.Integer, CultureInfo.InvariantCulture));

                case C.Terminals.Real:
                    return VisitConstant(double.Parse(node.Trim(), NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture));

                default:
                    throw new ParseException($"An unknown terminal was encountered. Found '{node.Parent.Name}'. Expected '{C.Terminals.Integer}' or '{C.Terminals.Real}'.");
            }
        }

        private static VariableExpression VisitVariable(Token node)
        {
            Guard.IsNotNull(node);

            return new VariableExpression(node.Trim());
        }

        private Expression Visit(object data)
        {
            Guard.IsNotNull(data);

            var node = data as Token;

            if (node == null)
            {
                var root = data as ReductionList;

                if (root == null)
                {
                    throw new ParseException("TODO");
                }

                switch (root.Parent.Head.Name)
                {
                    case C.Nonterminals.ConditionalAndExpression:
                        return VisitConditionalAnd(root);

                    case C.Nonterminals.ConditionalOrExpression:
                        return VisitConditionalOr(root);

                    case C.Nonterminals.DateConstantExpression:
                        return Visit(root.Single());

                    case C.Nonterminals.EqualityExpression:
                        return VisitEquality(root);

                    case C.Nonterminals.GroupExpression:
                        return VisitGroup(root);

                    case C.Nonterminals.InExpression:
                        return VisitIn(root);

                    case C.Nonterminals.LikeExpression:
                        return VisitLike(root);

                    case C.Nonterminals.NumericConstantExpression:
                        return Visit(root.Single());

                    case C.Nonterminals.ReferenceExpression:
                        return VisitReference(root.Single());

                    case C.Nonterminals.RelationalExpression:
                        return VisitRelational(root);

                    case C.Nonterminals.StringConstantExpression:
                        return Visit(root.Single());

                    case C.Nonterminals.UnaryExpression:
                        return VisitUnary(root);

                    case C.Nonterminals.VariableExpression:
                        return VisitVariable(root.Single());

                    default:
                        throw new ParseException($"An unknown nonterminal was encountered. Found '{root.Parent.Head.Name}'. Expected '{C.Nonterminals.ConditionalAndExpression}', '{C.Nonterminals.ConditionalOrExpression}', '{C.Nonterminals.DateConstantExpression}', '{C.Nonterminals.EqualityExpression}', '{C.Nonterminals.GroupExpression}', '{C.Nonterminals.InExpression}', '{C.Nonterminals.LikeExpression}', '{C.Nonterminals.NumericConstantExpression}', '{C.Nonterminals.ReferenceExpression}', '{C.Nonterminals.RelationalExpression}', '{C.Nonterminals.StringConstantExpression}', '{C.Nonterminals.UnaryExpression}', '{C.Nonterminals.VariableExpression}'.");
                }
            }

            switch (node.Parent.Name)
            {
                case C.Terminals.Date:
                    return VisitDateConstant(node);

                case C.Terminals.Integer:
                case C.Terminals.Real:
                    return VisitNumericConstant(node);

                case C.Terminals.String:
                    return VisitStringConstant(node);

                default:
                    throw new ParseException($"An unknown terminal was encountered. Found '{node.Parent.Name}'. Expected '{C.Terminals.Date}', '{C.Terminals.Integer}', '{C.Terminals.Real}' or '{C.Terminals.String}'.");
            }
        }

        private ConditionalAndExpression VisitConditionalAnd(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            var left = Visit(root.First().Data);

            var right = Visit(root.Last().Data);

            return new ConditionalAndExpression(left, right);
        }

        private ConditionalOrExpression VisitConditionalOr(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            var left = Visit(root.First().Data);

            var right = Visit(root.Last().Data);

            return new ConditionalOrExpression(left, right);
        }

        private EqualityExpression VisitEquality(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            var left = (ValueExpression) Visit(root.First().Data);

            var @operator = root.ElementAt(1).Data.ToString();

            var right = (ValueExpression) Visit(root.Last().Data);

            switch (@operator)
            {
                case C.Terminals.EqualityOperator:
                    return new EqualityExpression(left, EqualityOperator.EqualTo, right);

                case C.Terminals.InequalityOperator:
                    return new EqualityExpression(left, EqualityOperator.NotEqualTo, right);

                default:
                    throw new ParseException($"An unknown equality operator was encountered. Found '{@operator}'. Expected '{C.Terminals.EqualityOperator}' or '{C.Terminals.InequalityOperator}'.");
            }
        }

        private BooleanExpression VisitGroup(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            return (BooleanExpression) Visit(root.ElementAt(1).Data);
        }

        private InExpression VisitIn(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            var set = VisitTuple((ReductionList) root.ElementAt(3).Data);

            var value = (VariableExpression) Visit(root.First().Data);

            return new InExpression(value, set);
        }

        private LikeExpression VisitLike(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            var value = (VariableExpression) Visit(root.First().Data);

            var pattern = (ConstantExpression) Visit(root.Last().Data);

            return new LikeExpression(value, pattern);
        }

        private Expression VisitRelational(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            var left = (ValueExpression) Visit(root.First().Data);

            var @operator = root.ElementAt(1).Data.ToString();

            var right = (ValueExpression) Visit(root.Last().Data);

            switch (@operator)
            {
                case C.Terminals.GreaterThanOrEqualToRelationalOperator:
                    return new RelationalExpression(left, RelationalOperator.GreaterThanOrEqual, right);

                case C.Terminals.GreaterThanRelationalOperator:
                    return new RelationalExpression(left, RelationalOperator.GreaterThan, right);

                case C.Terminals.LessThanOrEqualToRelationalOperator:
                    return new RelationalExpression(left, RelationalOperator.LessThanOrEqual, right);

                case C.Terminals.LessThanRelationalOperator:
                    return new RelationalExpression(left, RelationalOperator.LessThan, right);

                default:
                    throw new ParseException($"An unknown relational operator was encountered. Found '{@operator}'. Expected '{C.Terminals.GreaterThanOrEqualToRelationalOperator}', '{C.Terminals.GreaterThanRelationalOperator}', '{C.Terminals.LessThanOrEqualToRelationalOperator}' or '{C.Terminals.LessThanRelationalOperator}'.");
            }
        }

        private Expression VisitReference(Token node)
        {
            Guard.IsNotNull(node);

            return Parse(_ruleTextResolver.Resolve(node.Trim('{', '}') /* reference rule name */, true /* throw exception if not found */));
        }

        private TupleExpression VisitTuple(ReductionList root)
        {
            Guard.IsNotEmpty(root);

            return new TupleExpression(root.AsEnumerable().Select(Visit).Cast<ConstantExpression>());
        }

        private Expression VisitUnary(ICollection<Token> root)
        {
            Guard.IsNotEmpty(root);

            var expression = (BooleanExpression) Visit(root.Last().Data);

            return new UnaryExpression(expression);
        }
    }
}