using System.Collections.Generic;
using Caprica.GOLDEngine.Parsing;
using Caprica.Helpers;

namespace Caprica.Infrastructure.Rules.Extensions
{
    internal static class TokenExtensions
    {
        public static IEnumerable<Token> AsEnumerable(this Token node)
        {
            Guard.IsNotNull(node);

            yield return node;
        }

        public static string Trim(this Token node, params char[] trimChars)
        {
            Guard.IsNotNull(node);

            return node.ToString().Trim('\'', '\"').Trim(trimChars);
        }
    }
}