using System.Collections.Generic;
using Caprica.Extensions;

namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     This class is used by the engine to hold a reduced rule. Rather the contain
    ///     a list of Symbols, a reduction contains a list of Tokens corresponding to the
    ///     the rule it represents. This class is important since it is used to store the
    ///     actual source program parsed by the Engine.
    /// </summary>
    public class ReductionList : List<Token>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Reduction" /> class.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        public ReductionList(int capacity)
            : base(capacity)
        {
            this.Initialize(default(Token), capacity);
        }

        /// <summary>
        ///     Gets the parent production.
        /// </summary>
        public Production Parent
        {
            get;
            internal set;
        }
    }
}