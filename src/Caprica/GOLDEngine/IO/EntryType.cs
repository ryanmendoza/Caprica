namespace Caprica.GOLDEngine.IO
{
    /// <summary>
    ///     Each record structure consists of a series of entries which, in turn, can hold any
    ///     number of data types. Preceding each entry is an identification byte which denotes
    ///     the type of data which is stored. Based on this information, the appropriate number
    ///     of bytes and the manner in which they are read can be deduced.
    /// </summary>
    public enum EntryType : byte
    {
        /// <summary>
        ///     A Boolean entry is preceded by a byte containing the value 66; the ASCII value for 'B'.
        ///     This entry is identical in structure to the Byte except the second byte will only
        ///     contain a 1, for true, or a 0 for false.
        /// </summary>
        Boolean = 66,

        /// <summary>
        ///     A "byte" entry is preceded by a single byte containing the value 98; the ASCII
        ///     value for 'b'. The next byte contains the actual information stored in the entry.
        ///     This is a rather inefficient method for storing a mass number of bytes given that
        ///     there is as much overhead as actual data. But, in the case of storing small
        ///     numbers, it does save a byte over using an integer entry.
        /// </summary>
        Byte = 98,

        /// <summary>
        ///     The entry only consists of an identification byte containing the value 69; the ASCII
        ///     value of 'E'. This type of entry is used to represent a piece of information that has
        ///     not been defined for reserved for future use. It has no actual value and should be
        ///     interpreted as a logical null.
        /// </summary>
        Empty = 69,

        /// <summary>
        ///     The default entry type if no other matches occur.
        /// </summary>
        Error = 0,

        /// <summary>
        ///     A string entry starts with a byte containing the value 83, which is the ASCII value
        ///     for "S". This is immediately followed by a sequence of 1 or more Unicode characters which are
        ///     terminated by a null.
        /// </summary>
        String = 83,

        /// <summary>
        ///     I - Unsigned, 2 byte
        ///     This is the most common entry used to store the Compiled Grammar Table information. Following
        ///     the identification byte, the integer is stored using Little-Endian byte ordering. In other
        ///     words, the least significant byte is stored first.
        /// </summary>
        UInt16 = 73
    }
}