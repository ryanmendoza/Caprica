using System;

namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     Each record describing a symbol in the Symbol Table is preceded by a byte containing the value 83 - the ASCII value of "S". The file
    ///     will contain one of these records for each symbol in the grammar. The Table Count record, which precedes any symbol records, will
    ///     contain the total number of symbols.
    /// </summary>
    [Flags]
    public enum SymbolTypes
    {
        /// <summary>
        ///     End Character - End of File (EOF). This symbol is used to represent the end of the file or the end of the source input.
        /// </summary>
        End = 3,

        /// <summary>
        ///     Error Terminal. If the parser encounters an error reading a token, this kind of symbol can used to differentiate it from other terminal types.
        /// </summary>
        Error = 7,

        /// <summary>
        ///     Lexical group end. Groups can end with normal terminals as well.
        /// </summary>
        GroupEnd = 5,

        /// <summary>
        ///     Lexical group start.
        /// </summary>
        GroupStart = 4,

        /// <summary>
        ///     Noise terminal. These are ignored by the parser. Comments and whitespace are considered 'noise'.
        /// </summary>
        Noise = 2,

        /// <summary>
        ///     Normal Nonterminal.
        /// </summary>
        Nonterminal = 0,

        /// <summary>
        ///     Normal Terminal.
        /// </summary>
        Terminal = 1
    }
}