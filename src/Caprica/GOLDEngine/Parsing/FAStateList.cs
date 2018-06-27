using System.Collections.Generic;
using Caprica.Extensions;

namespace Caprica.GOLDEngine.Parsing
{
    /// <inheritdoc />
    /// <summary>
    ///     TODO
    /// </summary>
    // ReSharper disable once InconsistentNaming
    internal class FAStateList : List<FAState>
    {
        /// <inheritdoc />
        /// <summary>
        ///     Initializes a new instance of the <see cref="T:Caprica.GOLDEngine.Parsing.FAStateList" /> class.
        /// </summary>
        /// <param name="capacity">The number of elements that the new list can initially store.</param>
        internal FAStateList(int capacity = 0)
            : base(capacity)
        {
            this.Initialize(default(FAState), capacity);

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