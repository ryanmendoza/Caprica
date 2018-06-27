namespace Caprica.GOLDEngine.IO
{
    /// <summary>
    ///     TODO
    /// </summary>
    public class Entry
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.IO.Entry" /> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="value">The value.</param>
        public Entry(EntryType type = EntryType.Empty, object value = null)
        {
            Type = type;

            Value = value;
        }

        /// <summary>
        ///     Gets or sets the type.
        /// </summary>
        /// <value>
        ///     The type.
        /// </value>
        public EntryType Type
        {
            get;
            set;
        }

        /// <summary>
        ///     Gets or sets the value.
        /// </summary>
        /// <value>
        ///     The value.
        /// </value>
        public object Value
        {
            get;
            set;
        }
    }
}