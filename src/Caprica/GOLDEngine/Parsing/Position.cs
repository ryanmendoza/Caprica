namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     TODO
    /// </summary>
    public class Position
    {
        /// <summary>
        ///     Gets or sets the column.
        /// </summary>
        /// <value>
        ///     The column.
        /// </value>
        public int Column
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the line.
        /// </summary>
        /// <value>
        ///     The line.
        /// </value>
        public int Line
        {
            get;
            set;
        }

        /// <summary>
        ///     Copies the specified position to this instance.
        /// </summary>
        /// <param name="position">The position to copy.</param>
        internal void Copy(Position position = null)
        {
            Column = position?.Column ?? 0;

            Line = position?.Line ?? 0;
        }
    }
}