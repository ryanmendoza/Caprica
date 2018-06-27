using System.Collections.Generic;
using Caprica.Extensions;

namespace Caprica.GOLDEngine.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     TODO
    /// </summary>
    internal class LRStateList : List<LRState>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.LRStateList" /> class.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        internal LRStateList(int capacity = 0)
            : base(capacity)
        {
            this.Initialize(default(LRState), capacity);

            InitialState = 0;
        }

        /// <summary>
        ///     Gets or sets the initial state.
        /// </summary>
        /// <value>
        ///     The initial state.
        /// </value>
        internal int InitialState
        {
            get;
            set;
        }
    }
}