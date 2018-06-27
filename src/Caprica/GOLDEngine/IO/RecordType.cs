namespace Caprica.GOLDEngine.IO
{
    /// <summary>
    ///     TODO
    /// </summary>
    internal enum RecordType : byte
    {
        /// <summary>
        ///     c
        /// </summary>
        CharRanges = 99,

        /// <summary>
        ///     D
        /// </summary>
        // ReSharper disable once InconsistentNaming
        DFAState = 68,

        /// <summary>
        ///     g
        /// </summary>
        Group = 103,

        /// <summary>
        ///     I
        /// </summary>
        InitialStates = 73,

        /// <summary>
        ///     L
        /// </summary>
        // ReSharper disable once InconsistentNaming
        LRState = 76,

        /// <summary>
        ///     R - R for Rule (related productions)
        /// </summary>
        Production = 82,

        /// <summary>
        ///     p
        /// </summary>
        Property = 112,

        /// <summary>
        ///     s
        /// </summary>
        Symbol = 83,

        /// <summary>
        ///     t - Table Counts
        /// </summary>
        TableCounts = 116
    }
}