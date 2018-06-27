namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     While the Symbol represents a class of terminals and nonterminals, the Token represents an individual
    ///     piece of information. Ideally, the token would inherit directly from the Symbol Class, but due to the fact
    ///     that Visual Basic 5/6 does not support this aspect of Object Oriented Programming, a Symbol is created as
    ///     a member and its methods are mimicked.
    /// </summary>
    public class Token
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Token" /> class.
        /// </summary>
        /// <param name="parent">The parent symbol of the token.</param>
        /// <param name="data">The data to  associate with the token.</param>
        public Token(Symbol parent = null, object data = null)
        {
            Data = data;

            Parent = parent;

            Position = new Position();

            State = 0;
        }

        /// <summary>
        ///     Gets or sets the data associated with the token.
        /// </summary>
        /// <value>
        ///     The data.
        /// </value>
        public object Data
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets the parent symbol of the token.
        /// </summary>
        public Symbol Parent
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Returns the line/column position where the token was read.
        /// </summary>
        /// <returns>
        ///     The line/column position where the token was read.
        /// </returns>
        public Position Position
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets or sets the state.
        /// </summary>
        /// <value>
        ///     The state.
        /// </value>
        internal int State
        {
            get;
            set;
        }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents the current object.
        /// </summary>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents the current object.
        /// </returns>
        public override string ToString()
        {
            return Data.ToString();
        }
    }
}