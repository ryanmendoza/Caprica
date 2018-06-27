using System.Collections.Generic;

namespace Caprica.GOLDEngine.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     TODO
    /// </summary>
    internal class LRState : List<LRAction>
    {
        /// <summary>
        ///     Gets or sets the <see cref="T:Caprica.GOLDEngine.Parsing.LRAction" /> with the specified symbol.
        /// </summary>
        /// <value>
        ///     The <see cref="LRAction" />.
        /// </value>
        /// <param name="symbol">The symbol to get or set.</param>
        /// <returns>
        ///     The <see cref="T:Caprica.GOLDEngine.Parsing.LRAction" /> assocaited with the specified <paramref name="symbol" />.
        /// </returns>
        public LRAction this[Symbol symbol]
        {
            get
            {
                var index = IndexOf(symbol);

                return index == -1 ? null : this[index];
            }

            set
            {
                var index = IndexOf(symbol);

                if (index != -1)
                {
                    this[index] = value;
                }
            }
        }

        /// <summary>
        ///     Returns the index of the first occurrence of the specified <paramref name="symbol" /> in a range of this list. The list
        ///     is searched forwards from beginning to end. The symbols of the list are compared to the given value using the
        ///     Object.Equals method.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <returns>
        ///     The zero-based index of the first occurrence of item within the entire list, if found; otherwise, –1.
        /// </returns>
        public int IndexOf(Symbol symbol)
        {
            return FindIndex(m => m.Symbol.Equals(symbol));
        }
    }
}