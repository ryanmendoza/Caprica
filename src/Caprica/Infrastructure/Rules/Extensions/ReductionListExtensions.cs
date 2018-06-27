using System;
using System.Collections.Generic;
using System.Linq;
using Caprica.GOLDEngine.Parsing;
using Caprica.Helpers;
using C = Caprica.Infrastructure.Rules.Helpers.Constants;

namespace Caprica.Infrastructure.Rules.Extensions
{
    internal static class ReductionListExtensions
    {
        public static IEnumerable<Token> AsEnumerable(this ReductionList root)
        {
            Guard.IsNotNull(root);

            return root.Where(WherePredicate).SelectMany(SelectManySelector);
        }

        private static IEnumerable<Token> SelectManySelector(Token token)
        {
            Guard.IsNotNull(token);

            return !(token.Data is ReductionList reduction) ? token.AsEnumerable() : reduction.AsEnumerable();
        }

        private static bool WherePredicate(Token token)
        {
            Guard.IsNotNull(token);

            return token.Data is ReductionList
                   || token.Parent.Name.Equals(C.Terminals.Date, StringComparison.OrdinalIgnoreCase)
                   || token.Parent.Name.Equals(C.Terminals.Integer, StringComparison.OrdinalIgnoreCase)
                   || token.Parent.Name.Equals(C.Terminals.Real, StringComparison.OrdinalIgnoreCase)
                   || token.Parent.Name.Equals(C.Terminals.String, StringComparison.OrdinalIgnoreCase);
        }
    }
}