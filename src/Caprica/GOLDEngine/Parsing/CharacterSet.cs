using System.Collections.Generic;
using Caprica.Extensions;

namespace Caprica.GOLDEngine.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     TODO
    /// </summary>
    internal class CharacterSet : List<CharacterRange>
    {
        /// <summary>
        ///     Determines whether the characterCode is in one of the ranges - and, therefore, the set. This method performs a linear
        ///     search; therefore, this method is an O(n) operation, where n is Count. The number of ranges in any given set are
        ///     relatively small - rarely exceeding 10 total. As a result, a simple linear search is sufficient rather than a binary
        ///     search. In fact, a binary search overhead might slow down the search!
        /// </summary>
        /// <param name="characterCode">The character code.</param>
        /// <returns>
        ///     <c>true</c> if the characterCode is in the set; otherwise, <c>false</c>.
        /// </returns>
        public bool Contains(int characterCode)
        {
            return Exists(m => characterCode.IsWithinRange(m.Start, m.End));
        }
    }
}