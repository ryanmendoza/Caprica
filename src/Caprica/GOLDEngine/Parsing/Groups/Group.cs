using System.Collections.Generic;

namespace Caprica.GOLDEngine.Parsing.Groups
{
    internal class Group
    {
        internal readonly List<int> Nesting;

        internal AdvanceModeType Advance;

        internal Symbol Container;

        internal Symbol End;

        internal EndingModeType Ending;

        internal string Name;

        internal Symbol Start;

        internal short TableIndex;

        internal Group()
        {
            Advance = AdvanceModeType.Character;

            Ending = EndingModeType.Closed;

            Nesting = new List<int>();
        }
    }
}