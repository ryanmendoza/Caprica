using System.Collections.ObjectModel;
using System.Linq;

namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     The Rule class is used to represent the logical structures of the grammar.
    ///     Rules consist of a head containing a nonterminal followed by a series of
    ///     both nonterminals and terminals.
    /// </summary>
    public class Production
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.Production" /> class.
        /// </summary>
        /// <param name="head">The head, or left-hand side, of the production.</param>
        /// <param name="index">The index of the production in the Production Table.</param>
        internal Production(Symbol head, int index)
        {
            Handle = new Collection<Symbol>();

            Head = head;

            Index = index;
        }

        /// <summary>
        ///     Gets the symbol list containing the handle (body), or right-hand side, of the production.
        /// </summary>
        public Collection<Symbol> Handle
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Gets the head, or left-hand side, of the production.
        /// </summary>
        public Symbol Head
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Gets the index of the production in the Production Table.
        /// </summary>
        /// <value>
        ///     The index of the production in the Production Table.
        /// </value>
        public int Index
        {
            get;
            internal set;
        }

        /// <summary>
        ///     Returns a <see cref="T:System.String" /> that represents this production in BNF.
        /// </summary>
        /// <param name="alwaysDelimitTerminals">if set to <c>true</c> [always delimit terminals].</param>
        /// <returns>
        ///     A <see cref="T:System.String" /> that represents this production in BNF.
        /// </returns>
        public string ToString(bool alwaysDelimitTerminals = false)
        {
            return Head.ToString() + " ::= " + string.Join(" ", Handle.Select(symbol => symbol.ToString(alwaysDelimitTerminals)));
        }

        /// <summary>
        ///     Determines whether this instance contains one nonterminal.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if contains one nonterminal; otherwise, <c>false</c>.
        /// </returns>
        internal bool ContainsOneNonTerminal()
        {
            return Handle.Count == 1 && Handle.First().Type == SymbolTypes.Nonterminal;
        }
    }
}