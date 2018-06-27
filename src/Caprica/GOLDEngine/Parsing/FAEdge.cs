namespace Caprica.GOLDEngine.Parsing
{
    /// <summary>
    ///     TODO
    /// </summary>
    internal class FAEdge
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.FAEdge" /> class.
        /// </summary>
        /// <param name="characters">The characters to advance on.</param>
        /// <param name="target">The target (FAState).</param>
        public FAEdge(CharacterSet characters, int target)
        {
            Characters = characters;

            Target = target;
        }

        /// <summary>
        ///     Gets the characters to advance on.
        /// </summary>
        /// <value>
        ///     The characters.
        /// </value>
        public CharacterSet Characters
        {
            get;
            private set;
        }

        /// <summary>
        ///     Gets the target (FAState).
        /// </summary>
        /// <value>
        ///     The target.
        /// </value>
        public int Target
        {
            get;
            private set;
        }
    }
}