namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     TODO
    /// </summary>
    internal enum ParseResultType
    {
        /// <summary>
        ///     TODO
        /// </summary>
        Accept = 1,

        /// <summary>
        ///     TODO
        /// </summary>
        InternalError = 6,

        /// <summary>
        ///     TODO
        /// </summary>
        ReduceEliminated = 4,

        /// <summary>
        ///     TODO
        /// </summary>
        ReduceNormal = 3,

        /// <summary>
        ///     TODO
        /// </summary>
        Shift = 2,

        /// <summary>
        ///     TODO
        /// </summary>
        SyntaxError = 5
    }
}