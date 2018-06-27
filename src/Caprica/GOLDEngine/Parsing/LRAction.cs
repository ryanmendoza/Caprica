namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     TODO
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class LRAction
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.LRAction" /> class.
        /// </summary>
        /// <param name="symbol">The symbol.</param>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public LRAction(Symbol symbol, LRActionType type, int value)
        {
            Symbol = symbol;

            Type = type;

            Value = value;
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

        /// <summary>
        ///     Gets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public LRActionType Type
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the value. Shift to state, reduce rule, goto state.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public int Value
        {
            get;
            private set;
        }
    }
}