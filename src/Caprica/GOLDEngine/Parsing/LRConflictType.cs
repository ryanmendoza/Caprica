namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     TODO
    /// </summary>
    internal enum LRConflictType
    {
        /// <summary>
        ///     TODO
        /// 
        ///     NOTE: Never happens with this implementation.
        /// </summary>
        AcceptReduce = 4,

        /// <summary>
        ///     TODO
        /// </summary>
        None = 5,

        /// <summary>
        ///     TODO
        /// </summary>
        ReduceReduce = 3,

        /// <summary>
        ///     TODO
        /// </summary>
        ShiftReduce = 2,

        /// <summary>
        ///     TODO
        /// 
        ///     NOTE: Never happens with this implementation.
        /// </summary>
        ShiftShift = 1
    }
}