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

        // ReSharper disable once NotAccessedField.Global
        internal string Name;

        internal Symbol Start;

        #pragma warning disable CS0649
        internal short TableIndex;
        #pragma warning restore CS0649

        internal Group()
        {
            Advance = AdvanceModeType.Character;

            Ending = EndingModeType.Closed;

            Nesting = new List<int>();
        }
    }
}