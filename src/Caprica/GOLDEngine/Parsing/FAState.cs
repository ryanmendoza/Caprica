using System.Collections.Generic;

namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     TODO
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class FAState
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.FAState" /> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        public FAState(Symbol symbol = null)
        {
            Edges = new List<FAEdge>();

            Symbol = symbol;
        }

        /// <summary>
        ///     Gets the edges.
        /// </summary>
        /// <value>
        ///     The edges.
        /// </value>
        public List<FAEdge> Edges
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the symbol.
        /// </summary>
        /// <value>
        ///     The symbol.
        /// </value>
        public Symbol Symbol
        {
            get;
            private set;
        }
    }
}