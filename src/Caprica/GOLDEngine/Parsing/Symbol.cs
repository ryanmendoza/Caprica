using Caprica.GOLDEngine.Parsing.Groups;

namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     This class is used to store of the nonterminals used by the Deterministic
    ///     Finite Automata (DFA) and LALR Parser. Symbols can be either
    ///     terminals (which represent a class of tokens - such as identifiers) or
    ///     nonterminals (which represent the rules and structures of the grammar).
    ///     Terminal symbols fall into several catagories for use by the GOLD Parser
    ///     Engine which are enumerated below.
    /// </summary>
    public class Symbol
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Symbol" /> class.
        /// </summary>
        /// <param name="name">The name of the symbol.</param>
        /// <param name="type">The type of the symbol.</param>
        /// <param name="index">The index of the symbol in the Symbol Table.</param>
        internal Symbol(string name, SymbolTypes type, int index)
        {
            Name = name;

            Index = index;

            Type = type;
        }

        /// <summary>
        ///     Gets the name of the symbol stored as a Unicode string.
        /// </summary>
        public string Name
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Gets the index of the symbol in the Symbol Table. The symbol should be stored directly at this Index.
        /// </summary>
        /// <value>
        ///     The index of the symbol in the Symbol Table.
        /// </value>
        public int Index
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Gets the type of the symbol.
        /// </summary>
        public SymbolTypes Type
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Gets or sets the group.
        /// </summary>
        /// <value>
        ///     The group.
        /// </value>
        internal Group Group
        {
            get;
            set;
        }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents this instance in BNF format..
        /// </summary>
        /// <param name="alwaysDelimitTerminals">if set to <c>true</c> [always delimit terminals].</param>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents this instance in BNF format..
        /// </returns>
        public string ToString(bool alwaysDelimitTerminals = false)
        {
            string result;

            switch (Type)
            {
                case SymbolTypes.Nonterminal:
                    result = "<" + Name + ">";

                    break;

                case SymbolTypes.Terminal:
                    result = LiteralFormat(Name, alwaysDelimitTerminals);

                    break;

                default:
                    result = "(" + Name + ")";

                    break;
            }

            return result;
        }

        /// <summary>
        ///     TODO
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="forceDelimit">if set to <c>true</c> [force delimit].</param>
        /// <returns>
        ///     TODO
        /// </returns>
        private static string LiteralFormat(string source, bool forceDelimit)
        {
            if (source == "'")
            {
                return "''";
            }

            short index = 0;

            while (index < source.Length & !forceDelimit)
            {
                var ch = source[index];

                forceDelimit = !(char.IsLetter(ch) | ch == '.' | ch == '_' | ch == '-');

                index += 1;
            }

            if (forceDelimit)
            {
                return "'" + source + "'";
            }

            return source;
        }
    }
}