namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     TODO
    /// </summary>
    internal enum LRActionType
    {
        /// <summary>
        ///     Input successfully parsed.
        /// </summary>
        Accept = 4,

        /// <summary>
        ///     Programmars see this often!
        /// </summary>
        Error = 5,

        /// <summary>
        ///     Goto to a state on reduction.
        /// </summary>
        Goto = 3,

        /// <summary>
        ///     Reduce by a specified rule.
        /// </summary>
        Reduce = 2,

        /// <summary>
        ///     Shift a symbol and goto a state.
        /// </summary>
        Shift = 1
    }
}